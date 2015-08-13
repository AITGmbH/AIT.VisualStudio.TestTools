namespace AIT.VisualStudio.TestTools.Composition
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Provides composition environment for tests.
    /// </summary>
    public abstract class TestCompositionContainerBase : IDisposable
    {
        private readonly Type[] _typesToRemove = new Type[0];

        private List<ComposablePartCatalog> _instancesToDispose;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCompositionContainerBase" /> class.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected TestCompositionContainerBase()
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCompositionContainerBase" /> class.
        /// </summary>
        /// <param name="typesToRemove">The types automatic remove.</param>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected TestCompositionContainerBase(params Type[] typesToRemove)
        {
            this._typesToRemove = typesToRemove;
            this.Initialize();
        }

        private List<ComposablePartCatalog> InstancesToDispose
        {
            get
            {
                if (this._instancesToDispose == null)
                {
                    this._instancesToDispose = new List<ComposablePartCatalog>();
                }

                return this._instancesToDispose;
            }
        }

        private CompositionContainer Container { get; set; }

        /// <summary>
        /// Gets the assemblies automatic include.
        /// </summary>
        protected virtual IEnumerable<Assembly> AssembliesToInclude
        {
            get
            {
                yield break;
            }
        }

        /// <summary>
        /// Gets the assemblies automatic include.
        /// </summary>
        protected virtual IEnumerable<Type> TypesToInclude
        {
            get
            {
                yield break;
            }
        }

        /// <summary>
        /// Gets the types to remove form the container.
        /// </summary>
        /// <remarks>
        /// These types are typically replaced with stubs.
        /// </remarks>
        protected virtual IEnumerable<Type> TypesToRemove
        {
            get
            {
                foreach (var type in this._typesToRemove)
                {
                    yield return type;
                }

                foreach (var stub in this.StubsToCompose)
                {
                    yield return stub.Type;
                }
            }
        }

        /// <summary>
        /// Gets the stubs to compose.
        /// </summary>
        protected virtual IEnumerable<Stub> StubsToCompose
        {
            get
            {
                yield break;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the exported value or the default.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public T GetExportedValueOrDefault<T>(string contractName = null)
        {
            if (Container == null)
            {
                this.Initialize();
            }

            if (this.Container != null)
            {
                try
                {
                    if (contractName == null)
                    {
                        return this.Container.GetExportedValue<T>();
                    }

                    return this.Container.GetExportedValue<T>(contractName);
                }
                catch (ReflectionTypeLoadException ex)
                {
                    foreach (var loaderException in ex.LoaderExceptions)
                    {
                        Trace.TraceError(loaderException.Message);
                    }

                    throw;
                }
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Satisfies the imports once.
        /// </summary>
        /// <param name="part">The part.</param>
        public void SatisfyImportsOnce(object part)
        {
            if (this.Container == null)
            {
                this.Initialize();
            }

            if (this.Container != null)
            {
                this.Container.SatisfyImportsOnce(part);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets the exported value or the default.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public IEnumerable<T> GetExportedValuesOrDefault<T>(string contractName = null)
        {
            if (this.Container == null)
            {
                this.Initialize();
            }

            if (this.Container != null)
            {
                if (contractName == null)
                {
                    return this.Container.GetExportedValues<T>();
                }
                return this.Container.GetExportedValues<T>(contractName);
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Adds the stub to the container.
        /// </summary>
        public void ComposeStub<T>(T exportedValue)
        {
            if (this.Container == null)
            {
                this.Initialize();
            }

            if (this.Container != null)
            {
                this.Container.ComposeExportedValue(exportedValue);
            }
        }

        /// <summary>
        /// Adds the stub to the container.
        /// </summary>
        protected void ComposeStub(Type type, object exportedValue)
        {
            if (this.Container == null)
            {
                this.Initialize();
            }

            if (this.Container != null)
            {
                var method =
                    typeof(AttributedModelServices).GetMethods()
                        .First(m => m.Name == "ComposeExportedValue" && m.GetParameters().Count() == 2)
                        .MakeGenericMethod(type);
                method.Invoke(this, new[] { this.Container, exportedValue });
            }
        }

        /// <summary>
        /// Adds the stub to the container.
        /// </summary>
        public void ComposeStub<T>(string contractName, T exportedValue)
        {
            if (this.Container == null)
            {
                this.Initialize();
            }

            if (this.Container != null)
            {
                this.Container.ComposeExportedValue(contractName, exportedValue);
            }
        }

        /// <summary>
        /// Adds the stub to the container.
        /// </summary>
        public void ComposeStubPart(object part)
        {
            if (this.Container == null)
            {
                this.Initialize();
            }

            if (this.Container != null)
            {
                this.Container.ComposeParts(part);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.InstancesToDispose.AsParallel().ForAll(i => i.Dispose());
                this.InstancesToDispose.Clear();

                if (this.Container != null)
                {
                    this.Container.Dispose();
                    this.Container = null;
                }
            }
        }

        private void Initialize()
        {
            var aggregateCatalog = new AggregateCatalog();

            this.InstancesToDispose.Add(aggregateCatalog);

            foreach (var assembly in this.AssembliesToInclude.Distinct())
            {
                var assemblyCatalog = new AssemblyCatalog(assembly);

                aggregateCatalog.Catalogs.Add(assemblyCatalog);
                this.InstancesToDispose.Add(assemblyCatalog);
            }

            foreach (var type in this.TypesToInclude.Distinct())
            {
                var typeCatalog = new TypeCatalog(type);

                aggregateCatalog.Catalogs.Add(typeCatalog);
                this.InstancesToDispose.Add(typeCatalog);
            }

            // Add a filter removing types from the test.
            Func<ComposablePartDefinition, bool> filter =
                (definition) =>
                this.TypesToRemove.All(type => definition.ExportDefinitions.All(e => e.ContractName != type.FullName));

            var filteredCatalog = new FilteredCatalog(aggregateCatalog, filter);

            this.InstancesToDispose.Add(filteredCatalog);

            this.Container = new CompositionContainer(
                filteredCatalog,
                CompositionOptions.DisableSilentRejection | CompositionOptions.ExportCompositionService
                | CompositionOptions.IsThreadSafe);

            this.Container.ComposeExportedValue(this.Container);

            foreach (var stub in this.StubsToCompose)
            {
                this.ComposeStub(stub.Type, stub.Instance);
            }

            this.OnInitialized();
        }

        /// <summary>
        /// Called after initialization of the <see cref="CompositionContainer" />.
        /// </summary>
        protected virtual void OnInitialized()
        {
        }
    }
}