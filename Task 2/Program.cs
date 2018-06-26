using System;
using System.IO;

namespace Task_2
{
    public class Program
    {
        /// <summary>
        /// Создание массива со множителями (для расчета факториалов)
        /// </summary>
        /// <param name="beg">Начальное значение</param>
        /// <param name="end">Конечное значение</param>
        /// <returns>Массив</returns>
        static int[] ProdArr(int beg, int end)
        {
            // Создаем массив
            int[] arr = new int[end - beg + 1];
            int j = 0;
            // Заполнеям от конечного значение до начального
            for (int i = end; i >= beg; i--)
            {
                arr[j] = i;
                j++;
            }
            return arr;
        }

        /// <summary>
        /// Сокращение множителей в числителе и знаменателе
        /// </summary>
        /// <param name="arrNum">Множители числителя</param>
        /// <param name="arrDen">Множители знаменателя</param>
        static void Reduct(ref int[] arrNum, ref int[] arrDen)
        {
            int size = arrDen.Length;
            // Перебираем значения множителей знаменателя
            for (int i = 0; i < size; i++)
            {
                int j = 0;
                // Ищем значение в числителе, которое делится на текущее значение знаменателя
                while (true)
                {
                    // Если находим, заменяем множитель числителя результатом деления, а знаменателя - 1
                    if (arrNum[j] % arrDen[i] == 0)
                    {
                        arrNum[j] = arrNum[j] / arrDen[i];
                        arrDen[i] = 1;
                        break;
                    }
                    j++;
                    // Прекращаем поиск текущего значения, если заканчивается массив
                    if (j == size - 1) break;
                }
            }
        }

        /// <summary>
        /// Вычисление кол-ва размещений для одного цвета шаров
        /// </summary>
        /// <param name="ball">Кол-во шаров</param>
        /// <param name="box">Кол-во коробок</param>
        /// <returns>Кол-во размещений</returns>
        static decimal NumPlace(int ball, int box)
        {
            // Если красные шары есть, то определяем кол-во размещений для них
            if (ball == 0) return 1;

            // Находим множители от факториала в числителе
            int[] arrNum = ProdArr(ball + 1, ball + box);
            // И в знаменателе
            int[] arrDen = ProdArr(1, box);

            // Сокращаем дробь
            Reduct(ref arrNum, ref arrDen);

            decimal result = 1; // Результат
            // Перемножаем оставшиеся множители числителя
            for (int i = 0; i < arrNum.Length; i++)
                result *= decimal.Parse(arrNum[i].ToString());
            // Делим на оставшиеся множители знаменателя
            for (int i = 0; i < arrDen.Length; i++)
                result /= arrDen[i];
            return result;
        }


        private static void Main(string[] args)
        {
            // Открываем файлы
            StreamReader fileRead = new StreamReader("input.txt");
            StreamWriter fileWrite = new StreamWriter("output.txt");

            // Считываем и определяем значения
            string[] str = fileRead.ReadLine().Split(' ');
            int N = int.Parse(str[0]);  // Кол-во коробок
            int A = int.Parse(str[1]);  // Кол-во красных шаров
            int B = int.Parse(str[2]);  // Кол-во синих шаров

            decimal resultA = NumPlace(A, N);        // Результат для красных шаров
            decimal resultB = NumPlace(B, N);        // Результат для синих шаров

            // Вычисляем общий результат
            decimal result = 0;
            if ((A == 0 && B == 0) || (N == 0)) result = 1;
            else result = resultA * resultB;

            // Выводим результат
            fileWrite.WriteLine(result);

            // Закрываем файлы
            fileRead.Close();
            fileWrite.Close();
        }
    }
}
