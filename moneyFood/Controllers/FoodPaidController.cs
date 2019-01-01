using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using moneyFood.Models;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace moneyFood.Controllers
{
    public class FoodPaidController : Controller
    {
        // GET: /<controller>/
        private testContext db;
        public FoodPaidController(testContext ctx)
        {
            db = ctx;
        }
        public async Task<IActionResult> Index()
        {
            var ps = await (from p in db.Food  orderby p.FoodId select p).ToListAsync();

            return View(ps);
        }
        public async Task<IActionResult> Details(int StudentId)
        {
            if (StudentId == 0)
            {
                return NotFound();

            }
            //var ps = await(from p in db.Food where p.FoodIdStudent == id orderby p.FoodId select p).ToListAsync();
            List<viewdetail> vpaidStu = await (from s in db.Students
                                               from cl in db.Class
                                               from en in db.EnrollStudentClass.Where(x => s.StudentId == x.StudentId && cl.ClassId == x.ClassId)
                                               from mo in db.Monthly.Where(x => cl.ClassYearIndex == x.MonthYear)
                                               from enpa in db.EnrollPaid.Where(x => s.StudentId == x.StudentId && mo.MonthlyId == x.MonthlyId).DefaultIfEmpty()
                                               where s.StudentId == StudentId
                                               orderby mo descending
                                               select new viewdetail
                                               {
                                                   StudentId = s.StudentId,
                                                   StudentTitle = s.StudentTitle,
                                                   StudentName = s.StudentName ?? "",
                                                   StudentLname = s.StudentLname,
                                                   MonthName = mo.MonthName,
                                                   MonthYear = mo.MonthYear,
                                                   MonthCourse = mo.MonthCourse,
                                                   ClassId = cl.ClassId,
                                                   Status = cl.Status,
                                                   ClassNameId = cl.ClassNameId,
                                                   TeacherId = cl.TeacherId,
                                                   ClassRoom = cl.ClassRoom,
                                                   ClassYearIndex = cl.ClassYearIndex,
                                                   PaidId = enpa.PaidId,
                                                   Paid = enpa.Paid == null ? 0 : enpa.Paid,
                                                   MonthlyId = mo.MonthlyId,
                                                   Timestamp = enpa.Timestamp.HasValue ? enpa.Timestamp : DateTime.Parse("19/02/1989"),
                                                   StaffPaidId = enpa.StaffPaidId == null ? 0 : enpa.StaffPaidId
                                               }).ToListAsync();
            
            if (vpaidStu == null)
            {
                return NotFound();
            }
            return View(vpaidStu);
        }




        public async Task<IActionResult> paid(int StudentId)
        {
            if (StudentId == 0)
            {
                return NotFound();

            }
            List<viewPaid> vpaidBystu = VpaidBystuFun(StudentId).Where(x => x.MonthCourse - x.totalPaid != 0).ToList();
            if (vpaidBystu == null)
            {
                return NotFound();
            }
            return View(vpaidBystu.OrderBy(x=>x.MonthlyId));
            //vpaidStu.GroupBy(x => x.MonthlyId).Select(x=>new {col1=x.Key(y=>y.)
        }
        public List<viewPaid> VpaidBystuFun(int StudentId)
        {
            List<viewdetail> vpaidStu = (from s in db.Students
                                              from cl in db.Class
                                              from en in db.EnrollStudentClass.Where(x => s.StudentId == x.StudentId && cl.ClassId == x.ClassId)
                                              from mo in db.Monthly.Where(x => cl.ClassYearIndex == x.MonthYear)
                                              from enpa in db.EnrollPaid.Where(x => s.StudentId == x.StudentId && mo.MonthlyId == x.MonthlyId).DefaultIfEmpty()
                                              where s.StudentId == StudentId
                                              orderby mo descending
                                              select new viewdetail
                                              {
                                                  StudentId = s.StudentId,
                                                  StudentTitle = s.StudentTitle,
                                                  StudentName = s.StudentName ?? "",
                                                  StudentLname = s.StudentLname,
                                                  MonthName = mo.MonthName,
                                                  MonthYear = mo.MonthYear,
                                                  MonthCourse = mo.MonthCourse,
                                                  ClassId = cl.ClassId,
                                                  Status = cl.Status,
                                                  ClassNameId = cl.ClassNameId,
                                                  TeacherId = cl.TeacherId,
                                                  ClassRoom = cl.ClassRoom,
                                                  ClassYearIndex = cl.ClassYearIndex,
                                                  PaidId = enpa.PaidId,
                                                  Paid = enpa.Paid == null ? 0 : enpa.Paid,
                                                  MonthlyId = mo.MonthlyId,
                                                  Timestamp = enpa.Timestamp.HasValue ? enpa.Timestamp : DateTime.Parse("19/02/1989"),
                                                  StaffPaidId = enpa.StaffPaidId == null ? 0 : enpa.StaffPaidId
                                              }).ToList();
            List<viewPaid> vpaidBystu = (vpaidStu.GroupBy(x => x.MonthlyId)
                                                .Select(y => new viewPaid
                                                {

                                                    StudentId = y.FirstOrDefault().StudentId,
                                                    StudentTitle = y.FirstOrDefault().StudentTitle,
                                                    StudentName = y.FirstOrDefault().StudentName,
                                                    StudentLname = y.FirstOrDefault().StudentLname,
                                                    MonthName = y.FirstOrDefault().MonthName,
                                                    MonthYear = y.FirstOrDefault().MonthYear,
                                                    MonthCourse = y.FirstOrDefault().MonthCourse,
                                                    ClassId = y.FirstOrDefault().ClassId,
                                                    Status = y.FirstOrDefault().Status,
                                                    ClassNameId = y.FirstOrDefault().ClassNameId,
                                                    TeacherId = y.FirstOrDefault().TeacherId,
                                                    ClassRoom = y.FirstOrDefault().ClassRoom,
                                                    ClassYearIndex = y.FirstOrDefault().ClassYearIndex,
                                                    PaidId = y.FirstOrDefault().PaidId,
                                                    totalPaid = y.Sum(x => x.Paid) == null ? 0 : y.Sum(x => x.Paid),
                                                    MonthlyId = y.FirstOrDefault().MonthlyId

                                                })
                                                    
                                               ).ToList();
            return vpaidBystu;
        }
        public List<viewPaid> VpaidFun()
        {
            List<viewdetail> vpaidStu = (from s in db.Students
                                         from cl in db.Class
                                         from en in db.EnrollStudentClass.Where(x => s.StudentId == x.StudentId && cl.ClassId == x.ClassId)
                                         from mo in db.Monthly.Where(x => cl.ClassYearIndex == x.MonthYear)
                                         from enpa in db.EnrollPaid.Where(x => s.StudentId == x.StudentId && mo.MonthlyId == x.MonthlyId).DefaultIfEmpty()
                                        // where s.StudentId == StudentId
                                         orderby mo descending
                                         select new viewdetail
                                         {
                                             StudentId = s.StudentId,
                                             StudentTitle = s.StudentTitle,
                                             StudentName = s.StudentName ?? "",
                                             StudentLname = s.StudentLname,
                                             MonthName = mo.MonthName,
                                             MonthYear = mo.MonthYear,
                                             MonthCourse = mo.MonthCourse,
                                             ClassId = cl.ClassId,
                                             Status = cl.Status,
                                             ClassNameId = cl.ClassNameId,
                                             TeacherId = cl.TeacherId,
                                             ClassRoom = cl.ClassRoom,
                                             ClassYearIndex = cl.ClassYearIndex,
                                             PaidId = enpa.PaidId,
                                             Paid = enpa.Paid == null ? 0 : enpa.Paid,
                                             MonthlyId = mo.MonthlyId,
                                             Timestamp = enpa.Timestamp.HasValue ? enpa.Timestamp : DateTime.Parse("19/02/1989"),
                                             StaffPaidId = enpa.StaffPaidId == null ? 0 : enpa.StaffPaidId
                                         }).ToList();
            List<viewPaid> vpaidBystu = (vpaidStu.GroupBy(x =>new { x.MonthlyId, x.StudentId })
                        
                                                .Select(y => new viewPaid
                                                {

                                                    StudentId = y.FirstOrDefault().StudentId,
                                                    StudentTitle = y.FirstOrDefault().StudentTitle,
                                                    StudentName = y.FirstOrDefault().StudentName,
                                                    StudentLname = y.FirstOrDefault().StudentLname,
                                                    MonthName = y.FirstOrDefault().MonthName,
                                                    MonthYear = y.FirstOrDefault().MonthYear,
                                                    MonthCourse = y.FirstOrDefault().MonthCourse,
                                                   // mustPay=y.FirstOrDefault().MonthCourse-( y.Sum(x => x.Paid) == null ? 0 : y.Sum(x => x.Paid)),
                                                    ClassId = y.FirstOrDefault().ClassId,
                                                    Status = y.FirstOrDefault().Status,
                                                    ClassNameId = y.FirstOrDefault().ClassNameId,
                                                    TeacherId = y.FirstOrDefault().TeacherId,
                                                    ClassRoom = y.FirstOrDefault().ClassRoom,
                                                    ClassYearIndex = y.FirstOrDefault().ClassYearIndex,
                                                    PaidId = y.FirstOrDefault().PaidId,
                                                    totalPaid = y.Sum(x => x.Paid) == null ? 0 : y.Sum(x => x.Paid),
                                                    MonthlyId = y.FirstOrDefault().MonthlyId

                                                })

                                               ).ToList();
            return vpaidBystu.Where(x=>x.MonthCourse-x.totalPaid!=0).ToList();
        }
        [AllowAnonymous]
        public IActionResult invoiceAllStudent()
        {
           
            List<viewPaid> vpaid = VpaidFun();
            ViewPrintInvoise detailinvoice = new ViewPrintInvoise();
            int themStudentId = 0;
            List<viewPaid> vPaidByStudent = new List<viewPaid>();
            
            foreach (var item in vpaid.OrderBy(c => c.StudentId).ThenBy(n => n.MonthlyId))
            {
                if (themStudentId != item.StudentId&& themStudentId!=0)
                {
                    var detaiilStudent = (from st in db.Students.Where(x => x.StudentId == themStudentId)
                                          join encl in db.EnrollStudentClass on st.StudentId equals encl.StudentId into encll
                                          from encl in encll.DefaultIfEmpty()
                                          join cl in db.Class on encl.ClassId equals cl.ClassId into clli
                                          from cl in clli.DefaultIfEmpty()
                                          select new studentNow
                                          {
                                              StudentId = st.StudentId,
                                              StudentTitle = st.StudentTitle,
                                              StudentName = st.StudentName,
                                              StudentLname = st.StudentLname,
                                              StudentBirthday = st.StudentBirthday,
                                              StudentIdcard = st.StudentIdcard,
                                              StudentPsisId = st.StudentPsisId,
                                              StudentStatus = st.StudentStatus,
                                              ClassId = cl.ClassId,
                                              Status = cl.Status,
                                              ClassNameId = cl.ClassNameId,
                                              TeacherId = cl.TeacherId,
                                              ClassRoom = cl.ClassRoom,
                                              ClassYearIndex = cl.ClassYearIndex
                                          }).ToList().OrderByDescending(x => x.ClassId).FirstOrDefault();
                    themStudentId = item.StudentId;
                    if (detailinvoice.detailStudent == null)
                    {
                        //It's null - create it
                        detailinvoice.detailStudent = new List<studentNow>();
                    }
                    if ( detailinvoice.studentInvoise == null)
                    {
                        //It's null - create it
                        detailinvoice.studentInvoise = new List<List<viewPaid>>();
                    }
                    detailinvoice.detailStudent.Add(  detaiilStudent );
                    detailinvoice.studentInvoise.Add(vPaidByStudent );
                    vPaidByStudent = new List<viewPaid>();
                }
                //else
                //{
                    vPaidByStudent.Add(item);
                    themStudentId = item.StudentId;
                //}
               

               // detailinvoice.studentInvoise.(item);
            }



            return new ViewAsPdf("invoiceAllStudent", detailinvoice)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A5,
                // PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
            //if (vpaid == null)
            //{
            //    return NotFound();
            //}
            //return View(detailinvoice);
            //vpaidStu.GroupBy(x => x.MonthlyId).Select(x=>new {col1=x.Key(y=>y.)
        }
        public List<viewdetail> VpaidBystubyMonId(int monId)
        {
                                  
            List<viewdetail> vpaidStu =       (from enpa in db.EnrollPaid.Where(x=>x.PayId==monId)
                                               join st in db.Students on enpa.StudentId equals st.StudentId into stl
                                               from st in stl.DefaultIfEmpty()
                                               from encl in db.EnrollStudentClass.Where(x=>st.StudentId == x.StudentId  && enpa.ClassId==x.ClassId)
                                               join cl in db.Class on encl.ClassId equals cl.ClassId into cll
                                               from cl in cll.DefaultIfEmpty()
                                               join mo in db.Monthly on enpa.MonthlyId equals mo.MonthlyId into mol
                                               from mo in mol.DefaultIfEmpty()
                                               select new viewdetail
                                         {
                                             StudentId = st.StudentId

                                             ,
                                                   StudentTitle = st.StudentTitle,
                                                   StudentName = st.StudentName ?? "",
                                                   StudentLname = st.StudentLname,
                                                  StudentIdcard=st.StudentIdcard,
                                                  StudentPsisId=st.StudentPsisId,
                                                   MonthName = mo.MonthName,
                                                   MonthYear = mo.MonthYear,
                                                   MonthCourse = mo.MonthCourse,
                                                   ClassId = cl.ClassId,
                                                   Status = cl.Status,
                                                   ClassNameId = cl.ClassNameId,
                                                   TeacherId = cl.TeacherId,
                                                   ClassRoom = cl.ClassRoom,
                                                   ClassYearIndex = cl.ClassYearIndex,
                                                   PaidId = enpa.PaidId,
                                                   Paid = enpa.Paid == null ? 0 : enpa.Paid,
                                                   MonthlyId = mo.MonthlyId,
                                                   Timestamp = enpa.Timestamp.HasValue ? enpa.Timestamp : DateTime.Parse("19/02/1989"),
                                                   StaffPaidId = enpa.StaffPaidId == null ? 0 : enpa.StaffPaidId
                                               }).ToList();
         
            return vpaidStu;
        }
        public async Task<IActionResult> ViewDetail()
        {
            var ps = await (from p in db.Students orderby p.StudentId select p).ToListAsync();
            var ps2 = await (from en in db.EnrollStudentClass
                             join cl in db.Class on en.ClassId equals cl.ClassId
                             where cl.ClassYearIndex.Equals(2561)
                             select new { cl, en.StudentId } into intermediate
                             join st in db.Students on intermediate.StudentId equals st.StudentId
                                
                             select new ViewStudentClass { students = st , Class2 =intermediate.cl }).ToListAsync();
            return View(ps2);
        }
        [HttpPost]
        public IActionResult AddPaidStudent([FromBody]AddPaidStudent model1)
        {
            int idPay = 0;
            using (var  transaction =db.Database.BeginTransaction())
            {
                try
                {
                    var cousePay = (from p in db.Monthly select p).ToList();
                    var EnrollPay = new EnrollPay
                    {
                        StudentId = model1.StudentId,
                        Pay = model1.numPaid,
                        Timestamp = DateTime.Now
                    };
                    db.EnrollPay.Add(EnrollPay);
                    db.SaveChanges();
                    var idPayFir = (from x in db.EnrollPay where x.Timestamp == EnrollPay.Timestamp select x.PayId);
                    List<viewPaid> couseUnpay = VpaidBystuFun(model1.StudentId);
                    idPay = idPayFir.Single();
                    int stuPay = model1.numPaid;
                    int pay = 0;
                    if (model1.MonthIdAndPaid.Length > 0)
                    {

                        foreach (int i in model1.MonthIdAndPaid)
                        {
                            var MonthCouse = couseUnpay.Where(x => x.MonthlyId == i).Select(x => x.MonthCourse).Single();
                            var classid = couseUnpay.Where(x => x.MonthlyId == i).Select(x => x.ClassId).Single();
                            var sumPaid = MonthCouse - couseUnpay.Where(x => x.MonthlyId == i).Select(x => x.PaidSum).Single();
                            if (stuPay > sumPaid)
                            {
                                pay =(int)sumPaid;
                                stuPay = stuPay - (int)sumPaid;
                            }
                            else if (stuPay == 0)
                            {
                                break;
                            }
                            else
                            {
                                pay = stuPay;
                                stuPay = 0;
                            }
                            var enrollPaid = new EnrollPaid
                            {
                                

                                StudentId = model1.StudentId,
                                MonthlyId = i,
                                Timestamp = DateTime.Now,
                                StudentTypeId = 1,
                                Paid = pay,
                                PayId = idPay,
                                ClassId= classid
                            };
                            db.EnrollPaid.Add(enrollPaid);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        foreach (int i in couseUnpay.Select(x=>x.MonthlyId))
                        {
                            var MonthCouse = couseUnpay.Where(x => x.MonthlyId == i).Select(x => x.MonthCourse).Single();
                            var classid = couseUnpay.Where(x => x.MonthlyId == i).Select(x => x.ClassId).Single();
                            var sumPaid = MonthCouse - couseUnpay.Where(x => x.MonthlyId == i).Select(x => x.PaidSum).Single();
                            if (stuPay > sumPaid)
                            {
                                pay = (int)sumPaid;
                                stuPay = stuPay - (int)sumPaid;
                            }
                            else if (stuPay == 0)
                            {
                                break;
                            }
                            else
                            {
                                pay = stuPay;
                                stuPay = 0;
                            }
                            var enrollPaid = new EnrollPaid
                            {


                                StudentId = model1.StudentId,
                                MonthlyId = i,
                                Timestamp = DateTime.Now,
                                StudentTypeId = 1,
                                Paid = pay,
                                PayId = idPay,
                                ClassId = classid

                            };
                            db.EnrollPaid.Add(enrollPaid);
                            db.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
              
            
           
         
            return Json(new { idpay = idPay });
        }
        public IActionResult printPaid(int enrollPayId)
        {
            List< viewdetail> listPay = VpaidBystubyMonId( enrollPayId);



            return View(listPay);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Food food)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Add(food);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["foodId"] = new Sel
        //}
        public IActionResult printBill(int enrollPayId)
        {
         
            
            List<viewdetail> listPay = VpaidBystubyMonId(enrollPayId);
            var detaiilStudent = (from st in db.Students.Where(x => x.StudentId == listPay.FirstOrDefault().StudentId)
                                  join encl in db.EnrollStudentClass on st.StudentId equals encl.StudentId into encll
                                  from encl in encll.DefaultIfEmpty()
                                  join cl in db.Class on encl.ClassId equals cl.ClassId into clli
                                  from cl in clli.DefaultIfEmpty()
                                  select new studentNow {
                                       StudentId=st.StudentId,
                                            StudentTitle=st.StudentTitle,
                                            StudentName=st.StudentName,
                                            StudentLname=st.StudentLname,
                                            StudentBirthday=st.StudentBirthday,
                                            StudentIdcard=st.StudentIdcard,
                                            StudentPsisId=st.StudentPsisId,
                                            StudentStatus=st.StudentStatus,
                                            ClassId=cl.ClassId,
                                            Status=cl.Status,
                                            ClassNameId=cl.ClassNameId,
                                            TeacherId=cl.TeacherId,
                                            ClassRoom=cl.ClassRoom,
                                            ClassYearIndex=cl.ClassYearIndex
                                  }).ToList().OrderByDescending(x => x.ClassId).First();
            BlogViewPrintBill detailBill = new BlogViewPrintBill
            {
                detailStudent = detaiilStudent,
                studentPaid = listPay
            };

            return new ViewAsPdf("printBill", detailBill)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A5,
                // PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
            //return View(detailBill);
        }
        public IActionResult DemoViewAsPDF()
        {
            return new ViewAsPdf("DemoViewAsPDF") {
                PageSize = Rotativa.AspNetCore.Options.Size.A5
            };
        }
    }
}
