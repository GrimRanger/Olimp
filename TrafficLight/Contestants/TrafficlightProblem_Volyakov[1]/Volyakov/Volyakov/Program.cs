using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrafficLight.Domain.Core.Interfaces;

namespace Volyakov
{
    class Program
    {
        static void Main(string[] args)     {
        }
        private static void TrafficlightProblem(ITrafficLight ligth)   {

#region Поля данных

            bool[,] Numbers = new bool[,] { 
                {true, true, true, false, true, true, true},
                {false,false,true,false,false,true,false},
                {true,false,true,true,true,false,true},
                {true,false,true,true,false,true,true},
                {false,true,true,true,false,true,false},
                {true,true,false,true,false,true,true},
                {true,true,false,true,true,true,true},
                {true,false,true,false,false,true,false},
                {true,true,true,true,true,true,true},
                {true,true,true,true,false,true,true}
            };
            bool nextLight;
            List<int> _leftCanBe = new List<int>();
            List<bool[]> _leftLooksLike=new List<bool[]>();
            bool[] _leftWorkingLamps = new bool[7];
            List<int> _rightCanBe = new List<int>();
            bool[] _rightWorkingLamps = new bool[7];

#endregion
#region Первый проход, задание полям начальных значений

            nextLight = ligth.GetNext();
            _leftLooksLike.Add(new bool[7]);
            for (int i=0;i<7;i++)
                _leftLooksLike[0][i] = ligth.Current.Item1[i];
            WorkingLamps(_leftWorkingLamps, _rightWorkingLamps, ligth);
            FirstRun(Numbers, ligth, _leftCanBe, _rightCanBe);

#endregion
        while (nextLight&&((_leftCanBe.Count!=1)||(_rightCanBe.Count!=1)))    {

            //Считываем следующий сигнал светофора
            if (!(nextLight = ligth.GetNext()))
                break;

            //Узнаем какие лампы в счетчиках точно работают
            WorkingLamps(_leftWorkingLamps, _rightWorkingLamps, ligth);

            //Проверяем изменение левого счетчика
            if (LeftLightChanged(_leftLooksLike, ligth))  {
                _rightCanBe = new List<int>();
                _rightCanBe.Add(9);
                ChangeLeftLight(ligth, Numbers,ref _leftCanBe,_leftLooksLike,_leftWorkingLamps);
                continue;

            }
            //Изменяем правый счетчик
            if (_rightCanBe.Count == 1) {
                if (_rightCanBe[0] != 0)
                    _rightCanBe[0] -= 1;
                else    {
                    _rightCanBe[0] = 9;
                    ChangeLeftLight(ligth, Numbers, ref _leftCanBe, _leftLooksLike, _leftWorkingLamps);
                }

            }
            else    {
                List<int> changes = new List<int>();
                for (int i = 0; i < _rightCanBe.Count; i++) {
                    bool theSameNumber = true;
                    if (_rightCanBe[i] != 0)    {
                        for (int j = 0; j < 7; j++)
                            if ((Numbers[_rightCanBe[i] - 1, j] == false && ligth.Current.Item2[j] == true)
                                ||(_rightWorkingLamps[j] == true && Numbers[_rightCanBe[i]-1,j] == true && ligth.Current.Item2[j] == false))    {
                                    theSameNumber = false;
                                    break;
                            }
                        if (theSameNumber)
                            changes.Add(_rightCanBe[i] - 1);

                    }
                    else    {
                        for (int j = 0; j < 7; j++)
                            if ((Numbers[9, j] == false && ligth.Current.Item2[j] == true)
                                || (_rightWorkingLamps[j] == true && Numbers[9, j] == true && ligth.Current.Item2[j] == false)) {
                                    theSameNumber = false;
                                    break;
                            }
                        if (theSameNumber)
                            changes.Add(9);

                    }
                }
                _rightCanBe = new List<int>(changes);
                if ((_rightCanBe.Count == 1) && (_rightCanBe[0] == 9))
                    ChangeLeftLight(ligth, Numbers, ref _leftCanBe, _leftLooksLike,_leftWorkingLamps);
            }
        }
#region Ответ на вопрос

            int result=0;
            if(nextLight) {
                result += _leftCanBe[0] * 10;
                result += _rightCanBe[0];
            }
            ligth.Answer(result);

#endregion
        }

#region Другие методы
        public static void ChangeLeftLight(ITrafficLight ligth, bool[,] Numbers,ref List<int> LeftCanBe,List<bool[]> look,bool[] LeftLamps)  {

            LeftCanBe.Remove(0);
            List<int> changes = new List<int>();
            if (LeftCanBe.Count == 1)
                LeftCanBe[0] -= 1;
            else    {
                //Оставляем только те элементы, что идут по убыванию и внешне похожи на текущий сигнал
                for (int i = 0; i < LeftCanBe.Count; i++) {
                    bool theSameNumber = true;
                    for (int j = 0; j < 7; j++)
                        if ((Numbers[LeftCanBe[i]-1, j] == false && ligth.Current.Item1[j] == true) 
                            ||  (LeftLamps[j]==true && Numbers[LeftCanBe[i]-1,j]==true && ligth.Current.Item1[j] ==false))    {
                            theSameNumber = false;
                            break;
                        }
                    if (theSameNumber)
                        changes.Add(LeftCanBe[i] - 1);

                }
                LeftCanBe = new List<int>(changes);
                changes = new List<int>();
                //Из отобранных элементов оставляем те, чьи предки правильно работали с учетом работующих ламп
                for (int i = 0; i < look.Count; i++)    {
                    for (int j = 0; j < LeftCanBe.Count; j++)   {
                        int lastNumber = LeftCanBe[j] + i + 1;
                        bool theSameNumber = true;
                        for (int k = 0; k < 7; k++) {
                            if (LeftLamps[k] == true && Numbers[lastNumber, k] == true && look[look.Count - 1 - i][k] == false) {
                                theSameNumber = false;
                                break;
                            }
                        }
                        if (theSameNumber && changes.Find(x => x==(lastNumber-i-1))==0)
                            changes.Add(LeftCanBe[j]);
                    }

                }
                LeftCanBe = new List<int>(changes);

            }
            //Добавляем последнее изображение в список
            look.Add(new bool[7]);
            for (int i = 0; i < 7; i++)
                look[look.Count-1][i] = ligth.Current.Item1[i];

        }
        public static void FirstRun(bool[,] Numbers, ITrafficLight light, List<int> LeftCanBe, List<int> RightCanBe)  {

            for (int i = 0; i < 10; i++)    {
                bool theSameNumber = true;
                for (int j = 0; j < 7; j++)
                    if ((Numbers[i, j] == false) && (light.Current.Item1[j] == true))   {
                        theSameNumber = false;
                        break;
                    }
                if (theSameNumber)
                    LeftCanBe.Add(i);

            }

            for (int i = 0; i < 10; i++)    {
                bool theSameNumber = true;
                for (int j = 0; j < 7; j++)
                    if ((Numbers[i, j] == false) && (light.Current.Item2[j] == true))   {
                        theSameNumber = false;
                        break;
                    }
                if (theSameNumber)
                    RightCanBe.Add(i);
            
            }
        }
        public static bool LeftLightChanged(List<bool[]> look, ITrafficLight light)    {
            bool[] itsNow = look[look.Count - 1];
            bool result=false;
            for (int i = 0; i < 7; i++)
                if (itsNow[i] != light.Current.Item1[i])    {

                    result = true;
                    break;
                }
            return result;
        }
        public static void WorkingLamps(bool[] LeftLamps, bool[] RightLamps, ITrafficLight light)   {

            for (int i = 0; i < 7; i++)
                if ((light.Current.Item1[i] == true) && (LeftLamps[i] == false))
                    LeftLamps[i] = true;
            for (int i = 0; i < 7; i++)
                if ((light.Current.Item2[i] == true) && (RightLamps[i] == false))
                    RightLamps[i] = true;
        }
#endregion
    }
}