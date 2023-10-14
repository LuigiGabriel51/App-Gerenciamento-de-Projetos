using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string CPF { get; set; }
        [NotNull]
        public string Email { get; set; }
        [NotNull]
        public DateTime Date { get; set; }
        [NotNull]
        public long Telefone { get; set; }
        [NotNull]
        public Sexo Sexo { get; set; }
        [NotNull]
        public string Office { get; set; }
        [NotNull]
        public string Password { get; set; }
        public byte[] Image { get; set; }
        [NotNull]
        public string Token { get; set; }
        [NotNull]
        public DateTime ExpireToken { get; set; }
        public Permission LevelPermission { get; set; }
    }
    public enum Sexo { Masculino = 1, Feminino, Neutro }
    public enum Permission { NV1 = 1, NV2, NV3 }
}