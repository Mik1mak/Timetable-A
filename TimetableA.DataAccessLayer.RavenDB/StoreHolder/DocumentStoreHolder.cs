using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TimetableA.DataAccessLayer.RavenDB.StoreHolder;

public class DocumentStoreHolder
{
    private readonly RavenDbSettings options;

    public IDocumentStore Store { get; private set; }
    public DocumentStoreHolder(IOptions<RavenDbSettings> options)
    {
        this.options = options.Value;
        Store = new DocumentStore()
        {
            Urls = this.options.Urls,
            Database = this.options.Database,
        };

        if (!string.IsNullOrEmpty(this.options.CertificatePath))
            ((DocumentStore)Store).Certificate = new X509Certificate2(this.options.CertificatePath);

        Store.Conventions.ReadBalanceBehavior = Raven.Client.Http.ReadBalanceBehavior.RoundRobin;
        Store.Initialize();
    }
}
