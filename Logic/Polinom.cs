using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class Polinom 
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
            for (int i = 0; i < p.coefficients.Length; i++)
            {
                p.coefficients[i] += value;
            }
            return p;
        }

        public static Polinom operator -(Polinom p, double value)
        {
            p += (-value);
            return p;
        }

        public static Polinom operator +(Polinom p1, Polinom p2)
        {
            if (p1 != null || p2 != null)
            {
                Polinom result;
                int maxLength = Math.Max(p1.coefficients.Length, p2.coefficients.Length);
                //result.coefficients = new double[maxLength];

                if (maxLength == p1.coefficients.Length)
                {
                    result = new Polinom(p1.coefficients);
                    for (int i = 0; i < result.coefficients.Length; i++)
                    {
                        result += p2.coefficients[i];
                    }
                }
                else
                {
                    result = new Polinom(p2.coefficients);
                    for (int i = 0; i < result.coefficients.Length; i++)
                    {
                        result += p1.coefficients[i];
                    }


                }
                return result;
            }
            return null;
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

        public double Calc(double value)
        {
            double sum = 0;
            for (int i = 0; i < this.coefficients.Length; i++)
                sum += this.coefficients[i] * Math.Pow(value, this.Power - i);
            return sum;
        }

    }
}
