namespace XCode.Core.Domain.Configuration
{
    /// <summary>
    /// ���ñ�
    /// </summary>
    public partial class Setting : BaseEntity
    {
        public Setting() { }
        
        public Setting(string name, string value) 
        {
            this.Name = name;
            this.Value = value;
        }
        
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ֵ
        /// </summary>
        public string Value { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
