using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Polinom 
    {
        private int x;
        private double[] coefficients;

        public int Power
        {
            get { return coefficients.Length-1; }
        }

        public Polinom()
        {

        }
        public double[] Coefficients
        {
            get { return coefficients; }
            set { coefficients = value; }
        }

        public Polinom(int power)
        {
            if (power <= 0)
                throw new ArgumentOutOfRangeException("power of polinom > 0");

            Random rand = new Random();

            for (int i = 0; i < Power + 1; i++)
            {
                coefficients[i] = rand.Next(-100, 100);
            }
        }

        public Polinom(double[] coefficients)
        {
            if (coefficients == null || coefficients.Length == 0)
                throw new ArgumentException("Incorrect coefficients");

            this.coefficients = new double[coefficients.Length];
            Array.Copy(coefficients, this.coefficients, coefficients.Length);
        }


        private void Simplify()
        {
            int pos = 0;
            while (pos < coefficients.Length && coefficients[pos] == 0)
                pos++;
            if (pos == coefficients.Length)
                coefficients = new double[] { 0 };
            else if (pos > 0)
            {
                double[] array = new double[coefficients.Length - pos];
                Array.Copy(coefficients, pos, array, 0, coefficients.Length - pos);
                coefficients = array;
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.coefficients.Length; i++)
                if (i == this.coefficients.Length - 1)
                    sb.Append(string.Format("{0}", coefficients[i]));
                else
                    sb.Append(string.Format("{0}x^{1} + ", coefficients[i], Power - i));
            return sb.ToString();
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Polinom))
                return false;
            else
                return this.coefficients == ((Polinom)obj).coefficients;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Polinom operator +(Polinom p, double value)
        {
            Polinom result = new Polinom();
            result.coefficients = new double[p.coefficients.Length];
            for (int i = 0; i < p.coefficients.Length; i++)
                result.coefficients[i] = i == p.coefficients.Length - 1 ? p.coefficients[i] + value : p.coefficients[i];
            return result;
        }
        public static Polinom operator -(Polinom p, double value)
        {
            return p + (-value);
        }
        public static Polinom operator +(Polinom p1, Polinom p2)
        {
            Polinom result = new Polinom();
            result.coefficients = new double[Math.Max(p1.coefficients.Length, p2.coefficients.Length)];
            int diff = p1.coefficients.Length - p2.coefficients.Length;
            for (int i = 0; i < result.coefficients.Length; i++)
                result.coefficients[i] = diff > 0 ?
                                            diff - i > 0 ? p1.coefficients[i] : p1.coefficients[i] + p2.coefficients[i - diff] :
                                            diff + i < 0 ? p2.coefficients[i] : p1.coefficients[diff + i] + p2.coefficients[i];
            result.Simplify();
            return result;
        }
        public static Polinom operator -(Polinom p1, Polinom p2)
        {
            return p1 + (p2 * (-1));
        }
        public static Polinom operator *(Polinom p, double value)
        {
            Polinom result = new Polinom();
            if (value == 0)
                result.coefficients = new double[] { 0 };
            else
            {
                result.coefficients = new double[p.coefficients.Length];
                for (int i = 0; i < p.coefficients.Length; i++)
                    result.coefficients[i] = p.coefficients[i] * value;
            }
            return result;
        }
        public static bool operator !=(Polinom p1, Polinom p2)
        {
            if (p1 == p2) return false;
            return true;
        }
        public static bool operator ==(Polinom p1, Polinom p2)
        {
            if (p1.coefficients.Length == p2.coefficients.Length)
            {
                for (int i = 0; i < p1.coefficients.Length; i++)
                {
                    if (p1.coefficients[i] != p1.coefficients[i]) return false;

                }
                return true;
            }

            return false;
        }
        public double Calc(double value)
        {
            double sum = 0;
            for (int i = 0; i < this.coefficients.Length; i++)
                sum += this.coefficients[i] * Math.Pow(value, this.Power - i);
            return sum;
        }

    }
}
