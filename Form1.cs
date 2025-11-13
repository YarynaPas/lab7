using System;
using System.Windows.Forms;

namespace lab7
{
    public enum stanMjacha
    {
        vGriA,      // м'яч у грі у команди А
        vGriB,      // м'яч у грі у команди В
        pozaGroju,  // м'яч поза грою
        vCentri,    // м'яч в центрі поля
        vVorotahA,  // м'яч у воротах команди А
        vVorotahB   // м'яч у воротах команди В
    }

    public partial class Form1 : Form
    {
        public static string newline = "\r\n";
        public static string sumText = "";
        public static string PlanText = "";
        public static int j = 1;

        public Form1()
        {
            InitializeComponent();
            button1.Click += button1_Click;
            button2.Click += button2_Click;
        }

        // === Базовий клас кулі ===
        public class Sphere
        {
            public const double Pi = Math.PI;
            double r, l, v, s, m;

            public double r_kuli
            {
                get { return r; }
                set
                {
                    r = value;
                    l = 2 * Pi * r;
                    v = 4 * Pi * r * r * r;
                    s = 4 * Pi * r * r;
                }
            }

            public double l_kola
            {
                get { return l; }
            }

            public double v_kuli
            {
                get { return v; }
            }

            public double s_kuli
            {
                get { return s; }
            }

            public double masa
            {
                get { return m; }
                set { m = value; }
            }

            public virtual double kotytys(double t, double v)
            {
                Form1.sumText += Form1.j.ToString() +
                                 ". Виконано метод kotytys(double, double) класу Sphere" +
                                 Form1.newline;
                Form1.j++;
                return 2 * Pi * r * t * v;
            }

            public virtual double letity(double t, double v)
            {
                Form1.sumText += Form1.j.ToString() +
                                 ". Виконано метод letity(double, double) класу Sphere" +
                                 Form1.newline;
                Form1.j++;
                return t * v;
            }

            public virtual double udar(double t, out double v, double f, double t1)
            {
                v = f * t1 / m;
                Form1.sumText += Form1.j.ToString() +
                                 ". Виконано метод udar(double, out double, double, double) класу Sphere" +
                                 " s = v * t = " + (t * v).ToString() +
                                 Form1.newline;
                Form1.j++;
                return t * v;
            }
        }

        // === Клас м'яча (нащадок Sphere) ===
        public class mjach : Sphere
        {
            public virtual void popav(bool je, ref int kilkist)
            {
                if (je) kilkist++;
                Form1.sumText += Form1.j.ToString() +
                                 ". Виконано метод popav(bool, ref int) класу mjach. kilkist=" +
                                 kilkist.ToString() + Form1.newline;
                Form1.j++;
            }

            public virtual double letity(double t, double v, double f_tertja)
            {
                Form1.sumText += Form1.j.ToString() +
                                 ". Виконано метод letity(double, double, double) класу mjach" +
                                 Form1.newline;
                Form1.j++;
                return t * v - f_tertja / masa * t * t / 2;
            }

            public double letityBase(double t, double v)
            {
                Form1.sumText += Form1.j.ToString() +
                                 ". Виконано метод letityBase(double, double) класу mjach" +
                                 Form1.newline;
                Form1.j++;
                return base.letity(t, v);
            }
        }

        // === Клас повітряної кулі (нащадок Sphere) ===
        public class povitrjana_kulja : Sphere
        {
            double tysk, maxTysk;

            public double tyskGazu
            {
                get { return tysk; }
                set { tysk = value; }
            }

            public double maxTyskGazu
            {
                get { return maxTysk; }
                set { maxTysk = value; }
            }

            public bool lopatys()
            {
                Form1.sumText += Form1.j.ToString() +
                                 ". Виконано метод lopatys() класу povitrjana_kulja" +
                                 Form1.newline;
                Form1.j++;
                return tysk > maxTysk;
            }

            public double letity(double t, double v, double v_Vitru, double kutVitru)
            {
                Form1.sumText += Form1.j.ToString() +
                                 ". Виконано метод letity(double, double, double, double) класу povitrjana_kulja" +
                                 Form1.newline;
                Form1.j++;
                return t * v - v_Vitru * Math.Sin(kutVitru);
            }

            public new double letity(double t, double v)
            {
                double s = t * v;
                Form1.sumText += Form1.j.ToString() +
                                 ". Виконано метод letity(double, double) класу povitrjana_kulja. Ми пролетіли " +
                                 s.ToString() + " метрів!" + Form1.newline;
                Form1.j++;
                return s;
            }
        }

        // === Клас футбольного м'яча (нащадок mjach) ===
        public class futbol_mjach : mjach
        {
            stanMjacha stanM;

            public stanMjacha tstanMjacha
            {
                get { return stanM; }
                set { stanM = value; }
            }

            public bool standout
            {
                get { return stanM == stanMjacha.pozaGroju; }
            }

            public futbol_mjach(stanMjacha sm)
            {
                Form1.sumText += Form1.j.ToString() +
                                 ". Виконано конструктор futbol_mjach(stanMjacha sm)" +
                                 Form1.newline;
                Form1.j++;

                stanM = sm;
                masa = 0.5F;
                r_kuli = 0.1F;
            }

            public void popav(bool je, ref int kilkistA, ref int kilkistB)
            {
                if (je)
                {
                    if (stanM == stanMjacha.vGriA) kilkistA++;
                    else if (stanM == stanMjacha.vGriB) kilkistB++;
                }

                Form1.sumText += Form1.j.ToString() +
                                 ". Виконано метод popav(bool, ref int, ref int) класу futbol_mjach" +
                                 Form1.newline;
                Form1.j++;
            }

            public void popavBase(bool b, ref int i)
            {
                Form1.sumText += Form1.j.ToString() +
                                 ". Виконано метод popavBase(bool, ref int) класу futbol_mjach, " +
                                 "який викликає метод popav(b, ref i) класу mjach" +
                                 Form1.newline;
                Form1.j++;
                base.popav(b, ref i);
            }

            public override void popav(bool je, ref int kilkist)
            {
                if (je) kilkist++;
                Form1.sumText += Form1.j.ToString() +
                                 ". Виконано метод popav(bool, ref int) (override) класу futbol_mjach" +
                                 Form1.newline;
                Form1.j++;
            }

            public override double letity(double t, double v, double ftertja)
            {
                Form1.sumText += Form1.j.ToString() +
                                 ". Виконано метод letity(double, double, double) (override) класу futbol_mjach" +
                                 Form1.newline;
                Form1.j++;
                return t * v - ftertja / masa * t * t / 2;
            }
        }

        // === Обробник кнопки Start ===
        private void button1_Click(object sender, EventArgs e)
        {
            j = 1;
            PlanText = "Планується зробити:" + newline;
            sumText = "Початок роботи" + newline;

            int i = 1;
            label2.Text = sumText + newline;

            PlanText += i.ToString() +
                        ". Плануємо створити екземпляр класу futbol_mjach, викликавши конструктор з параметром" +
                        newline;
            i++;

            futbol_mjach fm = new futbol_mjach(stanMjacha.vCentri);

            if (string.IsNullOrEmpty(sumText)) sumText = "";

            int GolA = 0, GolB = 0;

            fm.masa = 0.6F;
            fm.r_kuli = 0.12F;
            fm.tstanMjacha = stanMjacha.vGriA;

            PlanText += i.ToString() +
                        ". Плануємо викликати метод popav(true, ref GolA), перевизначений у класі futbol_mjach (override)" +
                        newline;
            i++;
            fm.popav(true, ref GolA);

            PlanText += i.ToString() +
                        ". Плануємо викликати метод popav(true, ref GolA, ref GolB), перевизначений у класі futbol_mjach з іншою сигнатурою" +
                        newline;
            i++;
            fm.popav(true, ref GolA, ref GolB);

            PlanText += i.ToString() + ", " + (i + 1).ToString() +
                        ". Плануємо через метод popavBase(true, ref GolA) викликати метод popav класу mjach" +
                        newline;
            i += 2;
            fm.popavBase(true, ref GolA);

            double s, v;

            PlanText += i.ToString() +
                        ". Плануємо викликати метод udar(2, out v, 200, 0.1) класу Sphere із класу futbol_mjach" +
                        newline;
            i++;
            s = fm.udar(2, out v, 200, 0.1);

            PlanText += i.ToString() +
                        ". Плануємо викликати метод kotytys(5, 1) класу Sphere із класу futbol_mjach" +
                        newline;
            i++;
            s = fm.kotytys(5, 1);

            PlanText += i.ToString() +
                        ". Плануємо викликати метод letity(20, 30) класу Sphere із класу futbol_mjach" +
                        newline;
            i++;
            s = fm.letity(20, 30);

            PlanText += i.ToString() +
                        ". Плануємо викликати перевизначений метод letity(20, 30, 5) класу futbol_mjach" +
                        newline;
            i++;
            s = fm.letity(20, 30, 5);

            label1.Text = PlanText;
            label2.Text = sumText;
        }

        // === Обробник кнопки Stop ===
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Порожній обробник кліку по label1 (бо він підв’язаний у дизайнері)
        private void label1_Click(object sender, EventArgs e)
        {
            // Можеш сюди нічого не писати, аби не було помилки
        }
    }
}
