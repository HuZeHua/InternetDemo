
namespace Anycmd
{
    using System;

    public class Coder
    {
        public Coder(string codespace, string code)
        {
            if (string.IsNullOrEmpty(codespace))
            {
                throw new ArgumentNullException("codespace");
            }
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }
            this.Codespace = codespace;
            this.Code = code;
        }

        /// <summary>
        /// 编码空间。
        /// <remarks>
        /// 给定一个“.”号分割的字符串“Anycmd.Person.Gender”，
        /// 最后个分割符后的是Code，最后一个分隔符前的是Codespace。
        /// </remarks>
        /// </summary>
        public string Codespace { get; private set; }

        /// <summary>
        /// 编码
        /// <remarks>
        /// 给定一个“.”号分割的字符串“Anycmd.Person.Gender”，
        /// 最后个分割符后的是Code，最后一个分隔符前的是Codespace。
        /// </remarks>
        /// </summary>
        public string Code { get; private set; }
    }
}
