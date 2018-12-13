using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DatabaseAccess;
namespace XMas_Project_WebAPI.Models
{
    public class InfoModel
    {
        public class Format_Detail
        {
            public int Id { get; set; }
            public string RandomCode { get; set; }
            public string PlayerName { get; set; }
            public string Desire { get; set; }
            public string Tip1 { get; set; }
            public string Tip2 { get; set; }
            public string Tip3 { get; set; }
            public bool Apply { get; set; }
            public bool Draw { get; set; }
            public int Receiver { get; set; }
            
        }

        public class Create_Detail
        {
            public string PlayerName { get; set; }
            public string Desire { get; set; }
            public string Tip1 { get; set; }
            public string Tip2 { get; set; }
            public string Tip3 { get; set; }
            public bool Apply { get; set; }
            public bool Draw { get; set; }
            public int Receiver { get; set; }
        }

        public class Privacy_Detail
        {
            public int Id { get; set; }
            public string PlayerName { get; set; }
            public bool Apply { get; set; }
            public bool Draw { get; set; }
        }

        public class ById_Detail
        {
            public int Id { get; set; }
            public string PlayerName { get; set; }
            public string Desire { get; set; }
            public string Tip1 { get; set; }
            public string Tip2 { get; set; }
            public string Tip3 { get; set; }

        }

        public class Tips_Detail
        {
            public string Tip1 { get; set; }
            public string Tip2 { get; set; }
            public string Tip3 { get; set; }
        }

        public class Apply_Detail
        {
            public string Desire { get; set; }
            public string Tip1 { get; set; }
            public string Tip2 { get; set; }
            public string Tip3 { get; set; }
        }

        public class Password_Detail
        {
            public string oldPassword { get; set; }
            public string newPassword { get; set; }
        }

        public class Status_Detail
        {
            public string Auth { get; set; }
            public bool Apply { get; set; }
            public bool Draw { get; set; }
        }




        public List<Format_Detail> GetAll()
        {
            using (XMas_DatabaseEntities entities = new XMas_DatabaseEntities())
            {
                var L2Enty = from c in entities.Info.AsNoTracking()
                             orderby c.Id
                             select c;

                return L2Enty.Select(s => new Format_Detail()
                {
                    Id = s.Id,
                    RandomCode = s.RandomCode,
                    PlayerName = s.PlayerName,
                    Tip1 = s.Tip1,
                    Tip2 = s.Tip2,
                    Tip3 = s.Tip3,
                    Apply = s.Apply,
                    Draw = s.Draw,
                    Receiver = s.Receiver,
                    Desire = s.Desire                 
                }).ToList<Format_Detail>();
            }
        }

        public List<Format_Detail> GetDetailById(int id, string randomCode)
        {
            using (XMas_DatabaseEntities entities = new XMas_DatabaseEntities())
            {

                var L2Enty = from c in entities.Info.AsNoTracking()
                             where c.Id == id && c.RandomCode.Contains(randomCode)
                             select c;

                return L2Enty.Select(s => new Format_Detail()
                {
                    Id = s.Id,
                    RandomCode = s.RandomCode,
                    PlayerName = s.PlayerName,
                    Desire = s.Desire,
                    Tip1 = s.Tip1,
                    Tip2 = s.Tip2,
                    Tip3 = s.Tip3
                }).ToList<Format_Detail>();
            }
        }

        public List<Privacy_Detail> GetAllPrivacy()
        {
            using (XMas_DatabaseEntities entities = new XMas_DatabaseEntities())
            {
                var L2Enty = from c in entities.Info.AsNoTracking()
                             orderby c.Id
                             select c;

                return L2Enty.Select(s => new Privacy_Detail()
                {
                    Id = s.Id,
                    PlayerName = s.PlayerName,
                    Apply = s.Apply,
                    Draw = s.Draw
                }).ToList<Privacy_Detail>();
            }
        }

        public bool RandomDraw()
        {
            List<int> IdList = new List<int>();
            using (XMas_DatabaseEntities entities = new XMas_DatabaseEntities())
            {
                var L2Enty = from c in entities.Info.AsNoTracking()
                             orderby c.Id
                             select c;

                var dataList = L2Enty.Select(s => new Format_Detail()
                {
                    Id = s.Id,
                }).ToList<Format_Detail>();


                foreach (var data in dataList)
                {
                    IdList.Add(data.Id);
                }
            }

            bool IsRandom = false;
            List<int> randomList = new List<int>();
            while (!IsRandom)
            {
                randomList = ShuffleList(IdList);
                for (int i = 0; i < IdList.Count; i++)
                {
                    if (IdList[i] == randomList[i])
                    {
                        IsRandom = false;
                        break;
                    }
                    if (i == IdList.Count - 1 && IdList[i] != randomList[i])
                        IsRandom = true;
                }
            }


            for (int i = 0; i < IdList.Count; i++)
            {
                int id = IdList[i];
                using (XMas_DatabaseEntities entities_new = new XMas_DatabaseEntities())
                {
                    var exist = entities_new.Info.Find(id);
                    exist.Receiver = randomList[i];
                    entities_new.SaveChanges();
                }
            }
            return true;
        }

        private List<int> ShuffleList(List<int> inputList)
        {
            List<int> DupicateList = new List<int>();
            DupicateList = inputList.ToList();
            List<int> randomList = new List<int>();

            Random r = new Random();
            int randomIndex = 0;
            while (DupicateList.Count > 0)
            {
                randomIndex = r.Next(0, DupicateList.Count); //Choose a random object in the list
                randomList.Add(DupicateList[randomIndex]); //add it to the new, random list
                DupicateList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }
    }
}