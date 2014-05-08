using BillsApplicationDomain.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BillsApplicationDomain.Services
{
    public interface IBillService
    {
        List<Bill> GetBills();
        void Add(Bill bill);
        Bill GetBillById(int id);
        Bill GetBillByName(string billName);
        void Update(Bill bill);
        void DeleteBillById(int id);
    }

    public class BillService : IBillService
    {
        private IGenericRepository _repo;

        public BillService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public List<Bill> GetBills()
        {
            try
            {
                return _repo.FindAll<Bill>().ToList();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("A List of Bills could not be returned. Error message: " + ex.Message);
                return null;
            }
            
        }

        public void Add(Bill bill)
        {
            bill.Name.ToLower();
            _repo.Create<Bill>(bill);
        }

        public Bill GetBillById(int id)
        {
            return _repo.GetById<Bill>(id);
        }

        public Bill GetBillByName(string billName)
        {
            return _repo.Find<Bill>(x => x.Name == billName.ToLower()).FirstOrDefault();
        }
       
        public void Update(Bill bill)
        {
            _repo.Update<Bill>(bill);
        }
               
        public void DeleteBillById(int id)
        {
            _repo.Delete<Bill>(id);
        }
    }
}