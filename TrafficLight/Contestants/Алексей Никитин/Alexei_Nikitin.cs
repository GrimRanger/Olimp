using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.Domain.Core.Interfaces;

namespace Алексей_Никитин
{
    public class Alexei_Nikitin
    {
        static Tuple<Digit, Digit> worked; //маска точно работающих светодиодиков
        static List<Tuple<Digit, Digit>> list; //Таблица полученных ответов на запрос
        static List<Digit> first; //Таблица хранящая набор рассово правильных положений для индикатора от 0 до 9
        static List<Tuple<Digit, Digit>> shablon; //Хорошо откормленная таблица first но уже от 0 до 99
        public static void Init()
        {
            worked = new Tuple<Digit, Digit>(new Digit(), new Digit());
            list = new List<Tuple<Digit, Digit>>();
            first = new List<Digit>();
            shablon = new List<Tuple<Digit, Digit>>();
            //Заполняем first          
            Digit d0, d1, d2, d3, d4, d5, d6, d7, d8, d9;
            //Создаём матрицу правильных индикаторов
            d0 = new Digit("1110111");
            d1 = new Digit("0010010");
            d2 = new Digit("1011101");
            d3 = new Digit("1011011");
            d4 = new Digit("0111010");
            d5 = new Digit("1101011");
            d6 = new Digit("1101111");
            d7 = new Digit("1010010");
            d8 = new Digit("1111111");
            d9 = new Digit("1111011");
            //Записываем нашу шматрицу
            first.Add(d0);
            first.Add(d1);
            first.Add(d2);
            first.Add(d3);
            first.Add(d4);
            first.Add(d5);
            first.Add(d6);
            first.Add(d7);
            first.Add(d8);
            first.Add(d9);

            //Заполняем большую маску
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    shablon.Add(new Tuple<Digit, Digit>(first[i], first[j]));
                }
            }
        }
        /*
         * Конечно можно было сделать массив текущих вариантов, а затем выбивать неправильные, и проверять оставшиеся...
         * Хотя реализация этой мысли и должна дать прирост производительности, но мысль поздно пришла на ум... 
         */
        public static List<int> Сhek()//Ищем кандидатов на текущее значение
        {
            List<int> out_list = new List<int>();

            //Первые 2 цикла переберают числа от 99 до 0
            for (int i = 9; i >= 0; i--)
            {
                for (int j = 9; j >= 0; j--)
                {
                    int flag = 1;   //по дефолту у нас всё хорошо

                    if ((i * 10 + j - list.Count) < 0)   //Если список полученных результатов больше чем оставшаяся часть маски
                    {
                        flag = 0;   //И не будет вашего цикла
                        break;
                    }

                    //Для каждого числа мы перебираем возможные варианты из полученных логов
                    for (int k = 0; (k < list.Count) && (flag == 1); k++)
                    {
                        //Перебираем светодиодики в индикаторе
                        for (int n = 0; n < 7; n++)
                        {
                            //Первый индикатор
                            if (worked.Item1.digit[k] == true)//этот светодиод работает
                            {
                                if (shablon[i * 10 + j].Item1.digit[n] != list[k].Item1.digit[n]) //У нас различие в результатах
                                {
                                    flag = 0;
                                    break;
                                }
                            }
                            //Второй индикатор
                            if (worked.Item2.digit[k] == true)//этот светодиод работает
                            {
                                if (shablon[i * 10 + j].Item2.digit[n] != list[k].Item2.digit[n]) //У нас различие в результатах
                                {
                                    flag = 0;
                                    break;
                                }
                            }
                        }
                    }
                    if (flag == 1)   //Все элементы маски совпали. Это наш подозреваемый на текущее число
                    {
                        out_list.Add(i * 10 + j - list.Count);
                    }
                }

            }
            return out_list;
        }
        public static void Slove(ITrafficLight light)
        {
            Init();
            bool flag = true;
            while (flag)
            {
                List<int> slove_t = new List<int>();
                flag = light.GetNext();
                if (!flag)   //Если с самого начала горит нуль
                {
                    light.Answer(0);
                    return;
                }
                var t = new Tuple<Digit, Digit>(new Digit(), new Digit());  //временный буффер
                try
                {
                    for (int i = 0; i < 7; i++)  //приводим во вменяемый вид входящие данные а так же уточняем маску рабочих элементов
                    {
                        //В лист
                        t.Item1.digit[i] = light.Current.Item1[i];
                        t.Item2.digit[i] = light.Current.Item2[i];
                        //В маску рабочих
                        if (light.Current.Item1[i] == true)
                            worked.Item1.digit[i] = true;
                        if (light.Current.Item2[i] == true)
                            worked.Item2.digit[i] = true;
                    }
                }
                catch (Exception e)//Если у нас входящий массив меньше 7 элементов, то это почти индикатор и мы почти даём правильный ответ
                {
                    light.Answer(-1);
                }
                list.Add(t);//Добавляем результат в полученные запрсы
                slove_t = Сhek();
                if (slove_t.Capacity == 1)
                {
                    light.Answer(slove_t[0]);
                    flag = false;
                    return;
                }
                if (slove_t.Capacity <= 0)
                {
                    light.Answer(-1);
                    flag = false;
                    return;
                }
            }
        }
    }
    class Digit //индикатор
    {
        public bool[] digit;
        public Digit() { digit = new bool[7]; }
        public Digit(Digit t)
        {
            digit = new bool[7];
            for (int i = 0; i < 7; i++)
            {
                digit[i] = t.digit[i];
            }
        }
        public Digit(string s)//Что бы не прописывать каждую ячейку маски вручную
        {
            if (s.Length == 7)
            {
                digit = new bool[7];
                for (int i = 0; i < 7; i++)
                {
                    if (s[i] == '1')
                        digit[i] = true;
                    else
                        digit[i] = false;
                }
            }
        }


    }



}
