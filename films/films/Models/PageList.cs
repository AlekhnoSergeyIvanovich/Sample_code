using System;
using System.Collections.Generic;
using System.Linq;

namespace films.Models
{
    public class PageList<T> : List<T>
    {
        //Общее количество страниц
        public int TotalPages { get; private set; }

        //Номер текущей страницы
        public int CurrentPage { get; private set; }

        //Конструктор
        PageList(List<T> items, int pageSize, int currentPage, int totalCount)
        {
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            CurrentPage = currentPage;
            this.AddRange(items);
        }

        public static PageList<T> CreatePageList(List<T> collection, int pageSize, int currentPage)
        {
            var count = collection.Count;
            var items = collection.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PageList<T>(items, pageSize, currentPage, count);
        }
    }
}