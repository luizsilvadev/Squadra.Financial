using System;

namespace Financial.WebAPI
{
    public class ContaPF
    {
        public int Id { get; set; }
        public int Agencia { get; set; }
        public int Conta { get; set; }
        public string TipoConta { get; set; }
        public string NomeCompleto { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            ContaPF objAsPart = obj as ContaPF;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return Id;
        }
        public bool Equals(ContaPF other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }
    }
}
