
namespace Anycmd.Edi.ViewModels.ArchiveViewModels
{
    using Engine.Edi;
    using Exceptions;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public partial class ArchiveTr
    {
        private OntologyDescriptor _ontology;
        private readonly IAcDomain _acDomain;

        public ArchiveTr(IAcDomain acDomain)
        {
            this._acDomain = acDomain;
        }

        public static ArchiveTr Create(ArchiveState archive)
        {
            return new ArchiveTr(archive.AcDomain)
            {
                ArchiveOn = archive.ArchiveOn,
                CreateOn = archive.CreateOn,
                CreateBy = archive.CreateBy,
                CreateUserId = archive.CreateUserId,
                DataSource = archive.DataSource,
                Id = archive.Id,
                NumberId = archive.NumberId,
                OntologyId = archive.OntologyId,
                Title = archive.Title,
                UserId = archive.UserId
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid OntologyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int NumberId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ArchiveOn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateOn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? CreateUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OntologyCode
        {
            get { return Ontology.Ontology.Code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OntologyName
        {
            get { return Ontology.Ontology.Name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CatalogName
        {
            get
            {
                var catalogName = string.Format(
                            "Archive{0}{1}_{2}",
                            Ontology.Ontology.Code,
                            this.ArchiveOn.ToString("yyyyMMdd"),
                            this.NumberId);
                return catalogName;
            }
        }
        private OntologyDescriptor Ontology
        {
            get
            {
                if (_ontology == null)
                {
                    if (!_acDomain.NodeHost.Ontologies.TryGetOntology(this.OntologyId, out _ontology))
                    {
                        throw new GeneralException("����ı����ʶ" + this.OntologyId);
                    }
                }
                return _ontology;
            }
        }
    }
}
