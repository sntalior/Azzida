using DAL;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Globalization;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.IO;
using Stripe;

using PushSharp.Apple;
using Newtonsoft.Json.Linq;

namespace BAL
{
    public class MasterUpdate : BusinessBase
    {
        string path = System.Configuration.ConfigurationManager.AppSettings["ImagePath"];

        public StripeCharge paymentId { get; private set; }

        public UserMaster Register(int Id, int RoleId, string FirstName, string LastName, string UserPassword,
            string UserEmail, string Skills, string DeviceId, string DeviceType, string EmailType, string UserName, string ProfilePicture, string JobType, string referalcode, string StripeAccId)
        {
            UserMaster um = new UserMaster();
            string VerifiedId = Guid.NewGuid().ToString("n").Substring(0, 6).ToUpper();

            if (Id > 0)
            {
                um = (from a in DB.UserMasters where a.Id == Id select a).FirstOrDefault();
                if (!string.IsNullOrEmpty(RoleId.ToString()))
                {
                    um.RoleId = RoleId;
                }
                if (!string.IsNullOrEmpty(FirstName))
                {
                    um.FirstName = FirstName;
                }
                if (!string.IsNullOrEmpty(LastName))
                {
                    um.LastName = LastName;
                }
                if (!string.IsNullOrEmpty(UserName))
                {
                    um.UserName = UserName;
                }
                if (!string.IsNullOrEmpty(UserPassword))
                {
                    um.UserPassword = UserPassword;
                }
                if (!string.IsNullOrEmpty(Skills))
                {
                    um.Skills = Skills;
                }
                if (!string.IsNullOrEmpty(DeviceId))
                {
                    um.DeviceId = DeviceId;
                }
                if (!string.IsNullOrEmpty(DeviceType))
                {
                    um.DeviceType = DeviceType;
                }
                if (!string.IsNullOrEmpty(EmailType))
                {
                    um.EmailType = EmailType.ToLower();
                }
                if (EmailType.ToLower() == "google")
                {
                    um.GoogleEmail = UserEmail;

                }
                else if (EmailType.ToLower() == "facebook")
                {
                    um.FaceBookEmail = UserEmail;

                }
                else
                {
                    um.GoogleEmail = "";
                    um.FaceBookEmail = "";
                }
                if (!string.IsNullOrEmpty(UserEmail))
                {
                    um.UserEmail = UserEmail;
                }

                if (!string.IsNullOrEmpty(ProfilePicture))
                {
                    um.ProfilePicture = ProfilePicture;
                }
                if (!string.IsNullOrEmpty(JobType))
                {
                    um.JobType = JobType;
                }
                else
                {
                    um.JobType = "";
                }

                if (!string.IsNullOrEmpty(StripeAccId))
                {
                    um.StripeAccId = StripeAccId;
                }
                um.ModifyDate = DateTime.Now;
            }
            else
            {
                string refCode = Guid.NewGuid().ToString("n").Substring(0, 6).ToUpper();
                um.RoleId = RoleId;
                um.FirstName = FirstName;
                um.LastName = LastName;
                um.UserPassword = UserPassword;
                um.UserName = UserName;
                um.Skills = Skills;
                um.DeviceId = DeviceId;
                um.DeviceType = DeviceType;
                um.EmailType = EmailType.ToLower();
                if (EmailType.ToLower() == "google")
                {
                    um.GoogleEmail = UserEmail;

                }
                else if (EmailType.ToLower() == "facebook")
                {
                    um.FaceBookEmail = UserEmail;

                }
                else
                {
                    um.GoogleEmail = "";
                    um.FaceBookEmail = "";
                }
                um.RefCode = refCode;
                um.UserEmail = UserEmail;
                um.ProfilePicture = ProfilePicture;
                um.CreatedDate = DateTime.Now;
                um.VerifiedId = VerifiedId;
                //bydefault true  jayega Active rhega
                um.IsActive = true;
                um.JobType = JobType;
                um.StripeAccId = StripeAccId;

                //if (referalcode != null)
                //{
                //    var referalcodeExist = IsRefCodeExist(referalcode);
                //    if (referalcodeExist != null)
                //    {
                //        ReferalBalance rb = new ReferalBalance();
                //        rb.UserId = Id;
                //        // rb.RefUserId = referalcodeExist.Id;
                //        rb.Amount = Convert.ToDecimal(10);
                //        rb.CreateDate = System.DateTime.Now;
                //        DB.ReferalBalances.InsertOnSubmit(rb);
                //        DB.SubmitChanges();


                //        var RefUserIdExist = (from a in DB.ReferalBalances where a.UserId == referalcodeExist.Id select a).FirstOrDefault();
                //        if (RefUserIdExist != null)
                //        {
                //            RefUserIdExist.Amount = RefUserIdExist.Amount + Convert.ToDecimal(10);
                //        }
                //        else
                //        {
                //            ReferalBalance rbu = new ReferalBalance();
                //            rbu.UserId = referalcodeExist.Id;

                //            rbu.Amount = Convert.ToDecimal(10);
                //            rbu.CreateDate = System.DateTime.Now;
                //            DB.ReferalBalances.InsertOnSubmit(rbu);

                //        }


                //        DB.SubmitChanges();
                //    }
                //}



                DB.UserMasters.InsertOnSubmit(um);
            }
            DB.SubmitChanges();

            if (Id == 0)
            {
                string str = "";
                //if (referalcode != null)
                //{
                //    str = str + "<div><p style='color:black !important'>" + "Hi " + um.FirstName.Trim() + "," + "</p>";

                //    str = str + "<p style='color:black !important'>" + "To verify your email click on link given below." + "</br></p>";
                //    str = str + "<p ><a style='color:#64c3a5 !important;' href='http://13.72.77.167:8085/VerifyEmail.aspx?VerifiedId=" + VerifiedId + "&ReferalCode=" + referalcode + "'>" + "http://13.72.77.167:8085/VerifyEmail.aspx?VerifiedId=" + VerifiedId + "&ReferalCode=" + referalcode + "</a></p>";
                //}
                //else
                //{
                //    str = str + "<div><p style='color:black !important'>" + "Hi " + um.FirstName.Trim() + "," + "</p>";

                //    str = str + "<p style='color:black !important'>" + "To verify your email click on link given below." + "</br></p>";
                //    str = str + "<p ><a style='color:#64c3a5 !important;' href='http://13.72.77.167:8085/VerifyEmail.aspx?VerifiedId=" + VerifiedId + "'>" + "http://13.72.77.167:8085/VerifyEmail.aspx?VerifiedId=" + VerifiedId + "</a></p>";

                //}





                string str1 = "<div><h2 style='color:blue !important'><img src='http://13.72.77.167:8085/ApplicationImages/LogoAzzida.png'/></h2></br>";
                //str1 = str1 + "<h5 style='color:blue !important;'>" + "Start Getting Stuff Done!" + " </h5></br>";
                if (referalcode != null)
                {
                    str1 = str1 + "<h5 style='color:black !important;'><a href='http://13.72.77.167:8085/VerifyEmail.aspx?VerifiedId=" + VerifiedId + "&ReferalCode=" + referalcode + "' ><img style='width:100px;' src='http://13.72.77.167:8085/ApplicationImages/cnfirm.png'/></a></h5></br>";
                    //str1 = str1 + "<h5 style='color:black !important;'><a href='http://13.72.77.167:8085/VerifyEmail.aspx?VerifiedId=" + VerifiedId + "&ReferalCode=" + referalcode + "' style='background-color:green !important; color:white !important; '><img src='C:/inetpub/AzzidaAdmin/dist/img/images.png'>Confirmation</a></h5></br>";
                }
                else
                {
                    str1 = str1 + "<h5 style='color:black !important;'><a href='http://13.72.77.167:8085/VerifyEmail.aspx?VerifiedId=" + VerifiedId + "' ><img style='width:100px;' src='http://13.72.77.167:8085/ApplicationImages/cnfirm.png'/></a></h5></br>";
                    //str1 = str1 + "<h5 style='color:black !important;'><a href='http://13.72.77.167:8085/VerifyEmail.aspx?VerifiedId=" + VerifiedId + "' style='background-color:green !important; color:white !important;font-size:17px;padding:9px; '><img src='D:/Soniya/Azzida/Azzida/AzzidaAdmin/dist/img/images.png'>Confirmation</a></h5></br>";
                }
                str1 = str1 + "<p style='color:black !important;font-size:17px;'>Dear " + FirstName + ", </p></br>";
                str1 = str1 + "<p style='color:black !important;font-size:17px;'> Thank you for registering with Azzida, the app that helps you get more done. My name is Lawrence Bunnell and I’m the CEO of Azzida.I’d like to welcome you and provide you with some information to help you get the most out of using the Azzida app.</p></br>";
                str1 = str1 + "<p style='color:black !important;font-size:17px;'> Azzida is the first mobile application that allows people with odd jobs, unskilled gigs and day labor needs to name their price and set a date for work to get done. With Azzida, you’re no longer at the mercy of having to pay fixed contractor or company rates to get things done. And, there’s no need for money to exchange hands as payment is handled through our mobile platform. </p></br>";
                str1 = str1 + "<p style='color:black !important;font-size:17px;'> For those who want to earn money helping others, Azzida provides a platform for finding work opportunities immediately available in your local community. As an Azzida Job Performer, there’s no need to pay for leads that don’t pan out, waste time preparing estimates or lowball your services to compete for jobs. With Azzida’s mobile app, you can search the real-time job feed for a wide variety of jobs immediately available in your area and earn money in ways suited to your time and talents; maybe even start or grow a small local business! And, once you’ve completed a job, your payment can be received on the same day through the app. </p></br>";
                str1 = str1 + "<p style='color:black !important;font-size:17px;'> With Azzida, you’re fostering a sense of community, bringing neighbors and neighborhoods closer together while improving lives – including your own. It’s often heard that technology separates us from one another. At Azzida, we believe it can also be a way to bring people closer together. </p></br>";
                str1 = str1 + "<p style='color:black !important;font-size:17px;'>To help you get started, we’ve created some useful answers to frequently asked questions in our <a href='http://azzida.com/odd_jobs/frequently-asked-questions/' style='color: blue !important;'>FAQ</a>. If you need additional assistance or have any suggestions on how to make Azzida even better, feel free to contact our team at <a href='mailto:support@azzida.com' style='color:blue !important;'>support@azzida.com. </a></p>";
                str1 = str1 + "<p style='color:black !important;font-size:17px;margin-block: 0px !important;'>Happy Odd Jobbing!</p>";
                str1 = str1 + "<p style='color:black !important;font-size:17px;margin-block: 0px !important;'>Lawrence Bunnell, CEO</p>";
                str1 = str1 + "<p style='color:black !important;font-size:17px;margin-block: 0px !important;'>Azzida</p>";

                str1 = str1 + "</div>";
                SendEMail("noreply@azzida.com", UserEmail, "Email Confirmation", str1);
            }
            return um;
        }

        public void AddReferalBalance(int Id, string referalcode)
        {
            var referalcodeExist = IsRefCodeExist(referalcode);
            if (referalcodeExist != null)
            {
                ReferalBalance rb = new ReferalBalance();
                rb.UserId = Id;
                // rb.RefUserId = referalcodeExist.Id;
                rb.Amount = Convert.ToDecimal(10);
                rb.CreateDate = System.DateTime.Now;
                DB.ReferalBalances.InsertOnSubmit(rb);
                DB.SubmitChanges();


                var RefUserIdExist = (from a in DB.ReferalBalances where a.UserId == referalcodeExist.Id select a).FirstOrDefault();
                if (RefUserIdExist != null)
                {
                    RefUserIdExist.Amount = RefUserIdExist.Amount + Convert.ToDecimal(10);
                }
                else
                {
                    ReferalBalance rbu = new ReferalBalance();
                    rbu.UserId = referalcodeExist.Id;

                    rbu.Amount = Convert.ToDecimal(10);
                    rbu.CreateDate = System.DateTime.Now;
                    DB.ReferalBalances.InsertOnSubmit(rbu);

                }


                DB.SubmitChanges();
            }
        }

        public UserMaster CheckverificationnId(string VerifiedId)
        {
            var data = (from a in DB.UserMasters
                        where a.VerifiedId == VerifiedId
                        /*a.UserEmail == Email*/

                        select a).FirstOrDefault();
            return data;
        }

        public void ConfirmEmail(string VerifiedId)
        {
            var data = (from a in DB.UserMasters
                        where a.VerifiedId == VerifiedId
                        //&& a.UserEmail == Email
                        select a).FirstOrDefault();
            if (data != null)
            {
                data.IsVerified = true;
                DB.SubmitChanges();
            }
        }
        public ApplicationStatus ApplicationAccepted(int JobId, int SeekerId, int ListerId, string IsAcceptedByLister)
        {
            ApplicationStatus ja = new ApplicationStatus();

            ja = (from a in DB.ApplicationStatus where a.JobId == JobId && a.ListerId == ListerId && a.SeekerId == SeekerId select a).FirstOrDefault();

            ja.IsAcceptedByLister = Convert.ToBoolean(IsAcceptedByLister);
            ja.ModifyDate = System.DateTime.Now;

            DB.SubmitChanges();

            var data = (from b in DB.ApplicationStatus where b.JobId == JobId && b.ListerId == ListerId && b.SeekerId != SeekerId select b).ToList();
            foreach (var s in data)
            {
                s.NotSelected = true;
                DB.SubmitChanges();
            }

            var receiverdata = (from um in DB.UserMasters
                                where um.Id == SeekerId
                                select new ReceiverData
                                {
                                    UserEmail = um.UserEmail,
                                    DeviceType = um.DeviceType,
                                    DeviceId = um.DeviceId,
                                    FullName = um.FirstName + " " + um.LastName,
                                    FirstName = um.FirstName,
                                    UserName = um.UserName

                                }).FirstOrDefault();
            var senderdata = (from u in DB.UserMasters
                              where u.Id == ListerId
                              select new SenderData
                              {
                                  SenderFullName = u.FirstName + " " + u.LastName,
                                  UserName = u.UserName,
                                  SenderProfilePicture = path + u.ProfilePicture
                              }).FirstOrDefault();
            var jobTitle = (from s in DB.Jobs where s.Id == JobId select s).FirstOrDefault();
            string applink1 = "";
            if (jobTitle.Applink == null)
            {
                applink1 = "";
            }
            else
            {
                applink1 = jobTitle.Applink + "?Performer";
            }

            string NotificationType = "Application Accepted";
            if (receiverdata.DeviceType.ToUpper() == "ANDROID")
            {
                SendNotification(receiverdata.DeviceId, ListerId.ToString(), SeekerId.ToString(), receiverdata.FullName, senderdata.SenderFullName + " has accepted your application for " + jobTitle.JobTitle, JobId, NotificationType, senderdata.SenderFullName, senderdata.SenderProfilePicture, "");
            }
            else
            {
                string otherparam = "\"FromUserId\":\"" + ListerId + "\",\"toUserId\":\"" + SeekerId + "\",\"fullName\":\"" + receiverdata.FullName + "\",\"JobId\":\"" + JobId + "\",\"NotificationType\":\"" + NotificationType + "\",\"SenderFullName\":\"" + senderdata.SenderFullName + "\",\"SenderProfilePicture\":\"" + senderdata.SenderProfilePicture + "\" ";
                string message = senderdata.SenderFullName + " has accepted your application for " + jobTitle.JobTitle;
                SendIhpone(receiverdata.DeviceId, otherparam, message);
            }
            string str = "<div><h2 style='color:blue !important'><img src='http://13.72.77.167:8085/ApplicationImages/LogoAzzida.png'/></h2></br>";
            //  str = str + "<h5 style='color:blue !important;'>" + "Start Getting Stuff Done!" + " </h5></br>";
            str = str + "<p style='color:black !important;'>Dear " + receiverdata.FirstName + ", </p></br>";
            str = str + "<p style='color:black !important;'>" + senderdata.SenderFullName + " accepted your application for " + '"' + jobTitle.JobTitle + '"' + ". To view the job posting " + "<a href='" + applink1 + "' style='color:blue !important;'>click here</a></p>"+".";
             str = str + "<p style='margin-block: 0px !important;'>The Team At Azzida</p>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>Email: <a href='mailto:support@azzida.com' style='color:blue !important;'>support@azzida.com</a></p>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>AppLink: <a href='https://play.google.com/store/apps/details?id=com.azzida' style='color:blue !important;'>https://play.google.com/store/apps/details?id=com.azzida</a></p>";
            str = str + "<p style='margin-block: 0px !important;'><a href='https://www.instagram.com/azzida_app' style='color:blue !important;'>Follow us on Instagram</a></p>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>Web: <a href='http://www.azzida.com/' style='color:blue !important;'>www.Azzida.com</a></p>";
            
            str = str + "</div>";

            SendEMail("noreply@azzida.com", receiverdata.UserEmail, NotificationType, str);
            return ja;
        }



        public List<GetApplicantList> GetViewApplicantList(int JobId)
        {
            //  ApplicationStatus ja = new ApplicationStatus();
            var JobCompleteCount = (from j in DB.Jobs
                                    where j.IsComplete.ToString() == "true"
                                    select j).ToList();
            var data1 = (from a in DB.ApplicationStatus
                         join um in DB.UserMasters
                         on a.SeekerId equals um.Id
                         where a.JobId == JobId
                         select new GetApplicantList
                         {
                             Id = a.Id,
                             ListerId = a.ListerId ?? 0,
                             SeekerId = um.Id,
                             FirstName = um.FirstName,
                             LastName = um.LastName,
                             ProfilePicture = um.ProfilePicture,
                             // UsrPrfl = path + um.ProfilePicture,
                             //  JobCompleteCount = JobCompleteCount.Count()

                         }).ToList();

            List<GetApplicantList> alist = new List<GetApplicantList>();
            GetApplicantList applicant = new GetApplicantList();
            foreach (var a in data1)
            {
                applicant = new GetApplicantList();
                applicant.Id = a.Id;
                applicant.FirstName = a.FirstName;
                applicant.LastName = a.LastName;
                applicant.ProfilePicture = path + a.ProfilePicture;
                applicant.ListerId = a.ListerId;
                applicant.SeekerId = a.SeekerId;
                applicant.JobCompleteCount = JobCompleteCount.Where(x => x.AssignSeekerId == a.SeekerId).Count();
                alist.Add(applicant);
            }
            return alist;

        }

        public GetPorfileDetail Getprofile(int UserId)
        {
            var data1 = (from r in DB.Tips
                         where r.SeekerId == UserId
                         select r).ToList();
            var Rating = (from rating in data1
                          select rating.SeekerRate).Sum();
            var RatingUserCount = data1.Count;

            double avg = (Convert.ToDouble(Rating) / Convert.ToDouble(data1.Count()));//count 
            if (double.IsNaN(avg))
            {
                avg = 0;
            }
            decimal receivedAmount = 0;
            var receivepayment = (from pa in DB.Payments where pa.ToUserId == UserId && pa.IsSeekerPaymentDone.ToString() == "true" select pa).ToList();
            foreach (var x in receivepayment)
            {
                receivedAmount = receivedAmount + x.SeekerPaymentAmount ?? 0;
            }

            var balance = (from b in DB.ReferalBalances where b.UserId == UserId select b.Amount).FirstOrDefault();
            var data = (from a in DB.UserMasters
                        where a.Id == UserId
                        select new GetPorfileDetail
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            ProfilePicture = a.ProfilePicture,
                            RoleId = a.RoleId,
                            Skills = a.Skills,
                            UserEmail = a.UserEmail,
                            UserPassword = a.UserPassword,
                            TokenId = a.TokenId,
                            UserName = a.UserName,
                            VerifiedId = a.VerifiedId,
                            Provider = a.Provider,
                            JobType = a.JobType,
                            IsVerified = a.IsVerified,
                            IsActive = a.IsActive,
                            GoogleEmail = a.GoogleEmail,
                            FaceBookEmail = a.FaceBookEmail,
                            EmailType = a.EmailType,
                            DeviceType = a.DeviceType,
                            RefCode = a.RefCode,
                            StripeAccId = a.StripeAccId,
                            AzzidaVerified = Convert.ToString(a.AzzidaVerified) == "true" ? true : false,
                            Balance = balance == null ? 0 : balance,
                            DeviceId = a.DeviceId,
                            CreatedDate = a.CreatedDate,
                            receivedAmount = a.UserReceivedAmount ?? 0,

                            UserRatingAvg = Math.Round(avg),
                        }).FirstOrDefault();
            //data.UserRatingAvg = avg;
            return data;
        }

        //public UserMaster IsExistFOrGId(string Email, string TokenId, string Provider)
        //{

        //    return data;
        //}

        public GetLoginDetail FacebookGoogleLogin(string Email, string UserName, string TokenId, string deviceId, string devicetype, string Provider)
        {
            UserMaster um = new UserMaster();
            um = (from a in DB.UserMasters
                  where a.UserEmail == Email
                  //&& a.UserName==UserName
                  //&& a.TokenId == a.TokenId
                  //&& a.Provider == Provider
                  select a).FirstOrDefault();
            if (um == null)
            {
                um = new UserMaster();
                string random = Guid.NewGuid().ToString("n").Substring(0, 6).ToUpper();
                string refCode = Guid.NewGuid().ToString("n").Substring(0, 6).ToUpper();
                um.UserEmail = Email;
                um.TokenId = TokenId;
                um.Provider = Provider;
                um.UserName = UserName;
                um.DeviceId = deviceId;
                um.DeviceType = devicetype;
                um.CreatedDate = DateTime.Now;
                um.VerifiedId = random;
                um.IsVerified = true;
                um.RefCode = refCode;
                DB.UserMasters.InsertOnSubmit(um);

            }
            else
            {
                um.DeviceId = deviceId;
                um.DeviceType = devicetype;
            }

            DB.SubmitChanges();
            var data1 = (from b in DB.UserMasters
                         where (b.UserEmail == Email)
                         //&& b.UserPassword == UserPassword
                         select new GetLoginDetail
                         {
                             Id = b.Id,
                             FirstName = b.FirstName,
                             LastName = b.LastName,
                             ProfilePicture = b.ProfilePicture,
                             RoleId = b.RoleId,
                             Skills = b.Skills,
                             UserEmail = b.UserEmail,
                             UserPassword = b.UserPassword,
                             TokenId = b.TokenId,
                             UserName = b.UserName,
                             VerifiedId = b.VerifiedId,
                             Provider = b.Provider,
                             JobType = b.JobType,
                             IsVerified = b.IsVerified,
                             IsActive = b.IsActive,
                             GoogleEmail = b.GoogleEmail,
                             FaceBookEmail = b.FaceBookEmail,
                             EmailType = b.EmailType,
                             DeviceType = b.DeviceType,
                             RefCode = b.RefCode == null ? "" : b.RefCode,
                             StripeAccId = b.StripeAccId,
                             AzzidaVerified = Convert.ToString(b.AzzidaVerified) == "true" ? true : false,
                             //  Balance = balance == null ? 0 : balance,
                             DeviceId = b.DeviceId,
                             CreatedDate = b.CreatedDate,
                             receivedAmount = b.UserReceivedAmount ?? 0,

                             //  UserRatingAvg = Math.Round(avg),
                         }).FirstOrDefault();

            return data1;

        }
        public ViewListerUser GetListerUser(int UserId)
        {
            //  ApplicationStatus ja = new ApplicationStatus();
            var JobCompleteCount = (from j in DB.Jobs
                                    where j.IsComplete.ToString() == "true" && j.AssignSeekerId == UserId
                                    select j).ToList();
            var JobPosting = (from j in DB.Jobs
                              where j.UserId == UserId
                              select j).ToList();

            var data1 = (from um in DB.UserMasters
                             //on a.ListerId equals um.Id
                         where um.Id == UserId
                         select new ViewListerUser
                         {

                             ListerId = um.Id,

                             Getrecentactivity = JobCompleteCount,
                             Name = um.FirstName + " " + um.LastName,
                             // lstnameUser = um.LastName,
                             joinDate = um.CreatedDate,
                             AzzidaVerified = um.AzzidaVerified == null ? false : um.AzzidaVerified,
                             ProfilePicture = path + um.ProfilePicture,
                             //UsrPrfl = path + um.ProfilePicture,
                             JobPostingcount = JobPosting.Count(),
                             JobCompleteCount = JobCompleteCount.Count()

                         }).FirstOrDefault();
            return data1;

        }
        public GetApplicantDetail GetViewApplicationDetail(int SeekerId)
        {
            // ApplicationStatus ja = new ApplicationStatus();
            var data = (from r in DB.Tips
                        where r.SeekerId == SeekerId
                        select r).ToList();
            var Rating = (from rating in data
                          select rating.SeekerRate).Sum();
            var RatingUserCount = data.Count;

            double avg = (Convert.ToDouble(Rating) / Convert.ToDouble(data.Count()));//count 
            if (double.IsNaN(avg))
            {
                avg = 0;
            }

            var JobCompleteCount = (from j in DB.Jobs
                                    where j.AssignSeekerId == SeekerId && j.IsComplete.ToString() == "true"
                                    select j).ToList();

            var data1 = (from um in DB.UserMasters
                             //on a.SeekerId equals um.Id
                         where um.Id == SeekerId
                         select new GetApplicantDetail
                         {
                             /*      Id = a.Id*/
                             SeekerId = um.Id,
                             SeekerName = um.FirstName + " " + um.LastName,
                             profilePicture = um.ProfilePicture,
                             UserProfile = path + um.ProfilePicture,
                             SkillExperience = um.Skills,
                             JobCompleteCount = JobCompleteCount.Count,
                             GetRecentActivity = JobCompleteCount,
                             RatingUserCount = RatingUserCount,
                             RateAvg = Math.Round(avg),
                             Joindate = um.CreatedDate,
                             AzzidaVarified = um.AzzidaVerified == null ? false : um.AzzidaVerified

                         }).FirstOrDefault();
            return data1;
            //return ja;
        }
        public ApplicationStatus OfferAccept(int JobId, int SeekerId, int ListerId, string IsAccteptedBySeeker)
        {
            ApplicationStatus ja = new ApplicationStatus();

            ja = (from a in DB.ApplicationStatus
                  where a.JobId == JobId
                  && a.SeekerId == SeekerId && a.ListerId == ListerId
                  select a).FirstOrDefault();

            ja.IsAccteptedBySeeker = Convert.ToBoolean(IsAccteptedBySeeker);
            ja.ModifyDate = System.DateTime.Now;

            DB.SubmitChanges();
            var data = (from j in DB.Jobs where j.Id == JobId select j).FirstOrDefault();// get table 
            if (data != null)
            {
                data.AssignSeekerId = SeekerId;//field to update api integration
                DB.SubmitChanges();
            }

            //job vali table update krani h seeker id 
            //Assignseekerid vale field me 


            var receiverdata = (from um in DB.UserMasters
                                where um.Id == ListerId
                                select new ReceiverData
                                {
                                    UserEmail = um.UserEmail,
                                    DeviceType = um.DeviceType,
                                    DeviceId = um.DeviceId,
                                    FullName = um.FirstName + " " + um.LastName,
                                    UserName = um.UserName

                                }).FirstOrDefault();
            var senderdata = (from u in DB.UserMasters
                              where u.Id == SeekerId
                              select new SenderData
                              {
                                  SenderFullName = u.FirstName + " " + u.LastName,
                                  UserName = u.UserName,
                                  SenderProfilePicture = path + u.ProfilePicture
                              }).FirstOrDefault();
            string NotificationType = "offer Accept";
            if (receiverdata.DeviceType.ToUpper() == "ANDROID")
            {
                SendNotification(receiverdata.DeviceId, SeekerId.ToString(), ListerId.ToString(), receiverdata.FullName, senderdata.SenderFullName + " has confirmed your offer.", JobId, NotificationType, senderdata.SenderFullName, senderdata.SenderProfilePicture, "");
            }
            else
            {
                string otherparam = "\"FromUserId\":\"" + SeekerId + "\",\"toUserId\":\"" + ListerId + "\",\"fullName\":\"" + receiverdata.FullName + "\",\"JobId\":\"" + 0 + "\",\"NotificationType\":\"" + NotificationType + "\",\"SenderFullName\":\"" + senderdata.SenderFullName + "\" ,\"SenderProfilePicture\":\"" + senderdata.SenderProfilePicture + "\"";
                string message = senderdata.SenderFullName + " has confirmed your offer.";
                SendIhpone(receiverdata.DeviceId, otherparam, message);
            }
            //string str = "<div><h2 style='color:blue !important'>AZZIDA</h2></br>";
            //str = str + "<h5 style='color:blue !important;'>" + "Start Getting Stuff Done!" + " </h5></br>";
            //str = str + "<p style='color:black !important;'>Dear " + receiverdata.FullName + ", </p></br>";
            //str = str + "<p style='color:black !important;'> " + senderdata.SenderFullName + "accepted your offer. Click here to see job details " + "<a href='" + applink + "' style='color:black !important;'>" + applink + "</a> </p></br>";
            //str = str + "<p><a href='https://www.facebook.com/azzidajobs/' style='color:black !important;'>Follow us on Facebook</a></p></br>";
            //str = str + "<p><a href='https://www.instagram.com/azzida_app' style='color:black !important;'>Follow us on Instagram</p></a></br>";
            //str = str + "<p style='color:black !important;'>Web: <a href='http://www.azzida.com/' style='color:blue !important;'>www.Azzida.com</a></p></br>";
            //str = str + "<p style='color:black !important;'>Email: <a href='support@azzida.com' style='color:blue !important;'>support@azzida.com</a></p></br>";
            //str = str + "</div>";
            //SendEMail("noreply@azzida.com", receiverdata.UserEmail, NotificationType, str);
            return ja;
        }
        public ApplicationStatus SaveApplicationStatus(int Id, int SeekerId, int ListerId, int JobId, string IsApply)
        {
            ApplicationStatus ja = new ApplicationStatus();

            ja = (from a in DB.ApplicationStatus where a.SeekerId == SeekerId && a.ListerId == ListerId && a.JobId == JobId && a.IsApply.ToString() == "true" select a).FirstOrDefault();
            //if (Id > 0)
            //{
            //    ja = (from a in DB.ApplicationStatus where a.Id == Id select a).FirstOrDefault();
            //    ja.SeekerId = SeekerId;
            //    ja.ListerId = ListerId;
            //    ja.JobId = JobId;
            //    ja.IsApply = Convert.ToBoolean(IsApply);
            //    ja.ModifyDate = DateTime.Now;
            //}
            //else
            //{
            if (ja == null)
            {
                ja = new ApplicationStatus();

                ja.SeekerId = SeekerId;
                ja.ListerId = ListerId;
                ja.JobId = JobId;
                ja.IsApply = Convert.ToBoolean(IsApply);
                ja.CreatedDate = DateTime.Now;
                ja.ModifyDate = System.DateTime.Now;
                DB.ApplicationStatus.InsertOnSubmit(ja);
            }
            DB.SubmitChanges();

            var seekerdata = (from a in DB.UserMasters
                              where a.Id == ListerId
                              select new ReceiverData
                              {
                                  UserEmail = a.UserEmail,
                                  DeviceId = a.DeviceId,
                                  DeviceType = a.DeviceType,
                                  FullName = a.FirstName,
                                  UserName = a.UserName
                              }).FirstOrDefault();

            var ListerData = (from b in DB.UserMasters
                              where b.Id == SeekerId
                              select new SenderData
                              {
                                  SenderFullName = b.FirstName + " " + b.LastName,
                                  UserName = b.UserName,
                                  SenderProfilePicture = path + b.ProfilePicture
                              }).FirstOrDefault();
            var jobtitle = (from jt in DB.Jobs where jt.Id == JobId select jt).FirstOrDefault();
            string applink1 = "";
            if (jobtitle.Applink == null)
            {
                applink1 = "";
            }
            else
            {
                applink1 = jobtitle.Applink + "?Poster";
            }
            string NotificationType = "Job Application";
            if (seekerdata.DeviceType.ToUpper() == "ANDROID")
            {
                SendNotification(seekerdata.DeviceId, SeekerId.ToString(), ListerId.ToString(), seekerdata.FullName, ListerData.SenderFullName + " has applied for your " + jobtitle.JobTitle, JobId, NotificationType, ListerData.SenderFullName, ListerData.SenderProfilePicture, "");
            }
            else
            {
                string otherparam = "\"FromUserId\":\"" + SeekerId + "\",\"toUserId\":\"" + ListerId + "\",\"fullName\":\"" + seekerdata.FullName + "\",\"JobId\":\"" + JobId + "\",\"NotificationType\":\"" + NotificationType + "\",\"SenderFullName\":\"" + ListerData.SenderFullName + "\",\"SenderProfilePicture\":\"" + ListerData.SenderProfilePicture + "\" ";
                string message = ListerData.SenderFullName + " has applied for your " + jobtitle.JobTitle;
                SendIhpone(seekerdata.DeviceId, otherparam, message);
            }
            string str = "<div><h2 style='color:blue !important'><img src='http://13.72.77.167:8085/ApplicationImages/LogoAzzida.png'/></h2></br>";
            //   str = str + "<h5 style='color:blue !important;'>" + "Start Getting Stuff Done!" + " </h5></br>";
            str = str + "<p style='color:black !important;'>Dear " + seekerdata.FullName + ", </p></br>";
            str = str + "<p style='color:black !important;'>" + ListerData.SenderFullName + " has applied for your job " + '"' + jobtitle.JobTitle + '"' + ". Click here to review " + "<a href='" + applink1 + "' style='color:blue !important;'>" + applink1 + "</a></p></br>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>The Team At Azzida</p>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>Email: <a href='mailto:support@azzida.com' style='color:blue !important;'>support@azzida.com</a></p>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>AppLink: <a href='https://play.google.com/store/apps/details?id=com.azzida' style='color:blue !important;'>https://play.google.com/store/apps/details?id=com.azzida</a></p>";
            str = str + "<p style='margin-block: 0px !important;'><a href='https://www.instagram.com/azzida_app' style='color:blue !important;'>Follow us on Instagram</a></p>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>Web: <a href='http://www.azzida.com/' style='color:blue !important;'>www.Azzida.com</a></p>";
            
            str = str + "</div>";



            SendEMail("noreply@azzida.com", seekerdata.UserEmail, NotificationType, str);
            return ja;
        }
        public Chat SaveChat(int Id, int ToId, int FromId, string IsTyping, string UserMessage, string MessageDateTime, int JobId)
        {
            Chat ja = new Chat();
            if (Id > 0)
            {
                ja = (from a in DB.Chats where a.Id == Id select a).FirstOrDefault();
                ja.ToId = ToId;
                ja.FromId = FromId;
                ja.IsTyping = Convert.ToBoolean(IsTyping);
                ja.UserMessage = UserMessage;
                ja.MessageDateTime = MessageDateTime;
                ja.JobId = JobId;
                ja.ModifyDate = DateTime.Now;
            }
            else
            {
                ja.ToId = ToId;
                ja.FromId = FromId;
                ja.IsTyping = Convert.ToBoolean(IsTyping);
                ja.UserMessage = UserMessage;
                ja.MessageDateTime = MessageDateTime;
                ja.CreatedDate = DateTime.Now;
                ja.JobId = JobId;
                DB.Chats.InsertOnSubmit(ja);
            }
            DB.SubmitChanges();

            var receiverdata = (from um in DB.UserMasters
                                where um.Id == ToId
                                select new ReceiverData
                                {
                                    UserEmail = um.UserEmail,
                                    DeviceType = um.DeviceType,
                                    DeviceId = um.DeviceId,
                                    FullName = um.FirstName + " " + um.LastName,
                                    UserName = um.UserName

                                }).FirstOrDefault();
            var senderdata = (from u in DB.UserMasters
                              where u.Id == FromId
                              select new SenderData
                              {
                                  SenderFullName = u.FirstName + " " + u.LastName,
                                  UserName = u.UserName,
                                  SenderProfilePicture = path + u.ProfilePicture
                              }).FirstOrDefault();

            //string applink1 = "";
            //var Jobdetail = (from a in DB.Jobs where a.Id == JobId select a).FirstOrDefault();
            //if (Jobdetail.Applink == null)
            //{
            //    applink1 = "";
            //}
            //else
            //{
            //    applink1 = Jobdetail.Applink + "?Performer";
            //}
            string NotificationType = "Chat";
            if (receiverdata.DeviceType.ToUpper() == "ANDROID")
            {
                SendNotification(receiverdata.DeviceId, FromId.ToString(), ToId.ToString(), receiverdata.FullName, senderdata.SenderFullName + " sent you message.", JobId, NotificationType, senderdata.SenderFullName, senderdata.SenderProfilePicture, "");
            }
            else
            {
                string otherparam = "\"FromUserId\":\"" + FromId + "\",\"toUserId\":\"" + ToId + "\",\"fullName\":\"" + receiverdata.FullName + "\",\"JobId\":\"" + JobId + "\",\"NotificationType\":\"" + NotificationType + "\",\"SenderFullName\":\"" + senderdata.SenderFullName + "\",\"SenderProfilePicture\":\"" + senderdata.SenderProfilePicture + "\"  ";
                string message = senderdata.SenderFullName + " sent you message.";
                SendIhpone(receiverdata.DeviceId, otherparam, message);
            }

            string str = "<div><h2 style='color:blue !important'><img src='http://13.72.77.167:8085/ApplicationImages/LogoAzzida.png'/></h2></br>";
            // str = str + "<h5 style='color:blue !important;'>" + "Start Getting Stuff Done!" + " </h5></br>";
            str = str + "<p style='color:black !important;'>Dear " + receiverdata.FullName + ", </p></br>";
            str = str + "<p style='color:black !important;'>" + senderdata.SenderFullName + " sent you message. </p></br>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>The Team At Azzida</p>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>Email: <a href='mailto:support@azzida.com' style='color:blue !important;'>support@azzida.com</a></p>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>AppLink: <a href='https://play.google.com/store/apps/details?id=com.azzida' style='color:blue !important;'>https://play.google.com/store/apps/details?id=com.azzida</a></p>";
            str = str + "<p style='margin-block: 0px !important;'><a href='https://www.instagram.com/azzida_app' style='color:blue !important;'>Follow us on Instagram</p></a>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>Web: <a href='http://www.azzida.com/' style='color:blue !important;'>www.Azzida.com</a></p>";
            //str = str + "<p style='color:black !important;margin-block: 0px !important;'>Email: <a href='mailto:support@azzida.com' style='color:blue !important;'>support@azzida.com</a></p>";
            str = str + "</div>";


            SendEMail("noreply@azzida.com", receiverdata.UserEmail, "Message", str);
            return ja;
        }

        public Chat SendAdminMessage(int fromUserId, int UserId, string message)
        {
            Chat ja = new Chat();

            //if (Id > 0)
            //{
            //    ja = (from a in DB.Chats where a.Id == Id select a).FirstOrDefault();
            //    ja.ToId = UserId;
            //    ja.FromId = FromId;
            //    ja.IsTyping = false;
            //    ja.UserMessage = message;
            //    ja.MessageDateTime = System.DateTime.Now.ToString();
            //    ja.ModifyDate = DateTime.Now;
            //}
            //else
            //{
            ja.ToId = UserId;
            ja.FromId = fromUserId;
            ja.IsTyping = false;
            ja.UserMessage = message;
            ja.MessageDateTime = System.DateTime.Now.ToString();
            ja.CreatedDate = DateTime.Now;
            DB.Chats.InsertOnSubmit(ja);
            //}
            DB.SubmitChanges();

            var receiverdata = (from um in DB.UserMasters
                                where um.Id == UserId
                                select new ReceiverData
                                {
                                    UserEmail = um.UserEmail,
                                    DeviceType = um.DeviceType,
                                    DeviceId = um.DeviceId,
                                    FullName = um.FirstName + " " + um.LastName,
                                    UserName = um.UserName

                                }).FirstOrDefault();
            var senderdata = (from u in DB.UserMasters
                              where u.Id == fromUserId
                              select new SenderData
                              {
                                  SenderFullName = u.FirstName + " " + u.LastName,
                                  UserName = u.UserName,
                                  SenderProfilePicture = path + u.ProfilePicture
                              }).FirstOrDefault();
            string NotificationType = "Send Message";
            if (receiverdata.DeviceType.ToUpper() == "ANDROID")
            {
                SendNotification(receiverdata.DeviceId, fromUserId.ToString(), UserId.ToString(), receiverdata.FullName, senderdata.UserName + " sent you message.", 0, NotificationType, senderdata.SenderFullName, senderdata.SenderProfilePicture, "");
            }
            else
            {
                string otherparam = "\"FromUserId\":\"" + fromUserId + "\",\"toUserId\":\"" + UserId + "\",\"fullName\":\"" + receiverdata.FullName + "\",\"JobId\":\"" + 0 + "\",\"NotificationType\":\"" + NotificationType + "\",\"SenderFullName\":\"" + senderdata.SenderFullName + "\",\"SenderProfilePicture\":\"" + senderdata.SenderProfilePicture + "\" ";
                string messageText = senderdata.SenderFullName + " sent you message.";
                SendIhpone(receiverdata.DeviceId, otherparam, messageText);
            }
            //string str = "<div><p style='color:black !important;'>" + senderdata.SenderFullName + " sent you message." + "</p>";
            //str = str + "<p style='color:black !important;'>" + message + " </p></div>";

            string str = "<div><h2 style='color:blue !important'><img src='http://13.72.77.167:8085/ApplicationImages/LogoAzzida.png'/></h2></br>";
            // str = str + "<h5 style='color:blue !important;'>" + "Start Getting Stuff Done!" + " </h5></br>";
            str = str + "<p style='color:black !important;'>Dear " + receiverdata.FullName + ", </p></br>";
            str = str + "<p style='color:black !important;'>" + senderdata.SenderFullName + " sent you message. " + message + "</p></br>";
            // str = str + "<p><a href='https://www.facebook.com/azzidajobs/' style='color:black !important;'>Follow us on Facebook</a></p></br>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>The Team At Azzida</p>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>Email: <a href='mailto:support@azzida.com' style='color:blue !important;'>support@azzida.com</a></p>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>AppLink: <a href='https://play.google.com/store/apps/details?id=com.azzida' style='color:blue !important;'>https://play.google.com/store/apps/details?id=com.azzida</a></p>";
            str = str + "<p style='margin-block: 0px !important;'><a href='https://www.instagram.com/azzida_app' style='color:blue !important;'>Follow us on Instagram</p></a>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>Web: <a href='http://www.azzida.com/' style='color:blue !important;'>www.Azzida.com</a></p>";
            //str = str + "<p style='color:black !important;margin-block: 0px !important;'>Email: <a href='mailto:support@azzida.com' style='color:blue !important;'>support@azzida.com</a></p>";
            str = str + "</div>";


            SendEMail("noreply@azzida.com", receiverdata.UserEmail, "Message", str);
            return ja;
        }

        public AndroidFCMPushNotificationStatus SendNotification(string deviceId, string from, string to, string fullname, string msg, int JobId, string NotificationType, string SenderFullName, string SenderProfilePicture, string FromDate = "")
        {
            AndroidFCMPushNotificationStatus result = new AndroidFCMPushNotificationStatus();

            try
            {
                string applicationID = "AAAAzCwSTjw:APA91bHgcn0SVAB2t5WLVQpl7yQHUbvv_tYJMe4jw68egvE_cnVN6sWfTTHTOymKvylv2M_cHcJTLsmHRqcZyt5j2YYAy7Zv2Idb8StHH1Mfz8ujoDfVREnn_llt0_JpwRtbXfVIt4Zq";
                string senderId = "876912725564";



                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var data = new
                {
                    to = deviceId,

                    data = new
                    {
                        title = "AzzidaApp",
                        message = msg,
                        priority = "high",
                        FromUserId = from,
                        ToUserId = to,
                        ReceiverUserName = fullname,
                        FromDate = FromDate,
                        NotificationType = NotificationType,
                        SenderFullName = SenderFullName,
                        SenderProfilePicture = SenderProfilePicture,
                        // AvailabiltyTimeTo = AvailabiltyTimeTo,
                        JobId = JobId

                    }

                };
                //var serializer = new Newtonsoft.Json.JsonSerializer();
                //var json = "{\"to\":\"" + deviceId + "\",\"data\":{\"body\":\"" + message + "\",\"title\":\"notification\",\"sound\":\"Enabled\"}}";
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return result;
        }



        public TipSuccessData Tipp(int Id, int UserId, int JobId, string TippingAmount, string TotalAmount, int SeekerId, decimal SeekerRate, int paymntId)
        {
            Tip t = new Tip();
            if (Id > 0)
            {
                t = (from a in DB.Tips where a.Id == Id select a).FirstOrDefault();
                t.UserId = UserId;
                t.JobId = JobId;
                t.TippingAmount = Convert.ToDecimal(TippingAmount);
                t.TotalAmount = Convert.ToDecimal(TotalAmount);
                t.SeekerId = SeekerId;
                t.SeekerRate = Math.Round(SeekerRate);
                t.ModifyDate = DateTime.Now;
            }
            else
            {
                t.UserId = UserId;
                t.JobId = JobId;
                t.TippingAmount = Convert.ToDecimal(TippingAmount);
                t.TotalAmount = Convert.ToDecimal(TotalAmount);
                t.SeekerId = SeekerId;
                t.SeekerRate = Math.Round(SeekerRate);
                t.CreatedDate = DateTime.Now;
                DB.Tips.InsertOnSubmit(t);
            }
            DB.SubmitChanges();


            string str = "";

            var paymentData = (from py in DB.Payments where py.Id == paymntId && py.PaymentType.ToLower() == "payment" select py).FirstOrDefault();
            var paymentTipData = (from py1 in DB.Payments where py1.JobId == paymentData.JobId && py1.PaymentType.ToLower() == "tip" select py1).FirstOrDefault();
            //try
            //{
            //    var ToUserAccountId = (from a in DB.UserMasters where a.Id == SeekerId select a).FirstOrDefault();
            var jobAmount = (from j in DB.Jobs where j.Id == JobId select j).FirstOrDefault();
            var fee = (from f in DB.JobFees select f).FirstOrDefault();

            //    var accountService = new StripeAccountService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);
            //    //"sk_test_QS7zwe52WufPIZHXjwnpgj1D");
            //    // StripeAccount account = accountService.Get("acct_19unI3J1Y72ANlMu");
            //    //acct_1CHlgSIoFMBSbD2s


            //    var ddd = new StripeTransferService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);
            //    //  bidamount = bidamount - (bidamount * Convert.ToDecimal(0.10));
            decimal paymentAmount = (jobAmount.Amount) - (jobAmount.Amount) * (fee.JobSeekerFee / 100) ?? 0;
            //  paymentAmount = paymentAmount + Convert.ToDecimal(TippingAmount);
            //    decimal paymentAmount = Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(TotalAmount) * (fee.JobSeekerFee / 100) ?? 0;


            //    if (!string.IsNullOrEmpty(ToUserAccountId.StripeAccId))
            //    {
            //        var myCharge12 = new StripeTransferCreateOptions
            //        {
            //            Amount = Convert.ToInt32(paymentAmount * Convert.ToDecimal(100)),
            //            Currency = "usd",
            //            //SourceTransaction =d.FirstOrDefault().TokenId,  // "txn_1CNImMIoFMBSbD2sPhE8O8co",
            //            Destination = ToUserAccountId.StripeAccId,//"acct_179XbqHpdOej4gWA",
            //                                                      // TransferGroup = jobid.ToString(),
            //                                                      // SourceType = "card"
            //            TransferGroup = JobId.ToString()

            //        };
            //        StripeTransfer stripeCharge1 = ddd.Create(myCharge12);

            if (paymentData != null)
            {
                paymentData.SeekerPaymentAmount = paymentAmount;
                paymentData.ModifyDate = System.DateTime.Now.ToString();
                paymentData.IsSeekerPaymentDone = true;
                DB.SubmitChanges();


                var tippaydata = (from ti in DB.Payments where ti.JobId == paymentData.JobId && ti.PaymentType.ToLower() == "tip" select ti).FirstOrDefault();
                if (tippaydata != null)
                {
                    tippaydata.IsSeekerPaymentDone = true;
                    DB.SubmitChanges();
                }

            }
            if (paymentTipData != null)
            {
                paymentTipData.SeekerPaymentAmount = Convert.ToDecimal(TippingAmount);
                paymentTipData.ModifyDate = System.DateTime.Now.ToString();
                paymentTipData.IsSeekerPaymentDone = true;
                DB.SubmitChanges();
            }
            //    }
            //    //string str = "Success";
            //}
            //catch (Exception ex)
            //{
            //    str = ex.Message;
            //}
            var resdata = (from tp in DB.Tips
                           where tp.Id == t.Id
                           select new TipSuccessData
                           {
                               Id = tp.Id,
                               JobId = tp.JobId,
                               UserId = tp.UserId,
                               SeekerId = tp.SeekerId,
                               SeekerRate = tp.SeekerRate,
                               TippingAmount = tp.TippingAmount,
                               TotalAmount = tp.TotalAmount,
                               Status = str == "" ? "Success" : str,
                               paymentId = paymntId,
                           }).FirstOrDefault();

            PaymentHistory ph = new PaymentHistory();
            ph.JobId = JobId;
            ph.UserId = UserId;
            ph.SeekerId = SeekerId;
            ph.paymentId = paymntId;
            ph.ListerPaymentAmount = paymentData.TotalAmount;
            ph.TippingAmount = Convert.ToDecimal(TippingAmount);

            ph.DisputeAmount = 0;

            //if (paymentId.Paid == true)
            //{
            ph.IsListerPaymentDone = true;

            //}
            //else
            //{
            //    ph.IsListerPaymentDone = false;

            //}
            if (string.IsNullOrEmpty(str))
            {
                ph.IsSeekerPaymentDone = true;
                ph.PaymentStatus = "success";

            }
            else
            {
                ph.IsSeekerPaymentDone = false;
                ph.PaymentStatus = "faild";
            }
            ph.CreatedDate = System.DateTime.Now.ToString();
            DB.PaymentHistories.InsertOnSubmit(ph);
            DB.SubmitChanges();
            //if (string.IsNullOrEmpty(str))
            //{

            var data = (from a in DB.Jobs where a.Id == t.JobId select a).FirstOrDefault();
            data.IscompleteUser = true;
            DB.SubmitChanges();

            //update seeker balance
            if (paymentTipData != null)
            {
                var UsrReceivedAmount = (from u in DB.UserMasters where u.Id == SeekerId select u).FirstOrDefault();
                UsrReceivedAmount.UserReceivedAmount = (UsrReceivedAmount.UserReceivedAmount == null ? 0 : UsrReceivedAmount.UserReceivedAmount) + paymentData.SeekerPaymentAmount + paymentTipData.SeekerPaymentAmount;
                DB.SubmitChanges();
            }
            ////ToSeekerPayment(paymntId, JobId, UserId, SeekerId, Convert.ToDecimal(TotalAmount));
            ////return t;

            //cash out is available 
            string NotificationType = "Job Complete";
            var receiverdata = (from um in DB.UserMasters
                                where um.Id == SeekerId
                                select new ReceiverData
                                {
                                    UserEmail = um.UserEmail,
                                    DeviceType = um.DeviceType,
                                    DeviceId = um.DeviceId,
                                    FullName = um.FirstName + " " + um.LastName,
                                    FirstName = um.FirstName,
                                    UserName = um.UserName

                                }).FirstOrDefault();
            var senderdata = (from u in DB.UserMasters
                              where u.Id == UserId
                              select new SenderData
                              {

                                  SenderFullName = u.FirstName + " " + u.LastName,
                                  UserName = u.UserName,
                                  SenderProfilePicture = path + u.ProfilePicture
                              }).FirstOrDefault();
            string applink1 = "";
            if (jobAmount.Applink == null)
            {
                applink1 = "";
            }
            else
            {
                applink1 = jobAmount.Applink + "?Performer";
            }
            string NotificationType1 = "Cash Available";
            if (receiverdata.DeviceType.ToUpper() == "ANDROID")
            {
                SendNotification(receiverdata.DeviceId, UserId.ToString(), SeekerId.ToString(), senderdata.SenderFullName, receiverdata.FullName + " your cashout is available.", JobId, NotificationType, senderdata.SenderFullName, senderdata.SenderProfilePicture, "");
            }
            else
            {
                string otherparam = "\"FromUserId\":\"" + UserId + "\",\"toUserId\":\"" + SeekerId + "\",\"fullName\":\"" + senderdata.SenderFullName + "\",\"JobId\":\"" + JobId + "\",\"NotificationType\":\"" + NotificationType + "\",\"SenderFullName\":\"" + senderdata.SenderFullName + "\",\"SenderProfilePicture\":\"" + senderdata.SenderProfilePicture + "\" ";
                string message = receiverdata.FullName + " your cashout is available.";
                SendIhpone(receiverdata.DeviceId, otherparam, message);
            }

            string str1 = "<div><h2 style='color:blue !important'><img src='http://13.72.77.167:8085/ApplicationImages/LogoAzzida.png'/></h2></br>";
            // str1 = str1 + "<h5 style='color:blue !important;'>" + "Start Getting Stuff Done!" + " </h5></br>";
            str1 = str1 + "<p style='color:black !important;'>Congratulations, you have funds available for transfer to your bank account at Azzida! To cash out, open the Azzida mobile app, go to " + '"' + "My Profile" + '"' + " and select " + '"' + "Cash Out" + '"' + ".</p></br>";
            str1 = str1 + "<p style='color:black !important;'>Cheers!</p></br>";
            str1 = str1 + "<p style='color:black !important;'>The Team at Azzida</p></br>";
            //str1 = str1 + "<p style='color:black !important;'>" + " Your cashout is available for " + '"' +  jobAmount.JobTitle + '"' + ". Click here to review " + " <a href = '" + applink1 + "' style = 'color:blue !important;' > " + applink1 + " </a></p></br>";
            //str1 = str1 + "<p style='color:black !important;'>" + " Your cashout is available for " + '"' +  jobAmount.JobTitle + '"' + ". Click here to review " + " <a href = '" + applink1 + "' style = 'color:blue !important;' > " + applink1 + " </a></p></br>";
            // str1 = str1 + "<p><a href='https://www.facebook.com/azzidajobs/' style='color:black !important;'>Follow us on Facebook</a></p></br>";
            str1 = str1 + "<p style='color:black !important;margin-block: 0px !important;'>Email: <a href='mailto:support@azzida.com' style='color:blue !important;'>support@azzida.com</a></p>";
            str1 = str1 + "<p style='color:black !important;margin-block: 0px !important;'>AppLink: <a href='https://play.google.com/store/apps/details?id=com.azzida' style='color:blue !important;'>https://play.google.com/store/apps/details?id=com.azzida</a></p>";
            str1 = str1 + "<p style='margin-block: 0px !important;'><a href='https://www.instagram.com/azzida_app' style='color:blue !important;'>Follow us on Instagram</a></p>";
            str1 = str1 + "<p style='color:black !important;margin-block: 0px !important;'>Web: <a href='http://www.azzida.com/' style='color:blue !important;'>www.Azzida.com</a></p>";

            str1 = str1 + "</div>";


            SendEMail("noreply@azzida.com", receiverdata.UserEmail, NotificationType1, str1);


            return resdata;
        }
        public Rate RatePost(int UserId, int JobId, decimal Rate, int SeekerId)
        {
            Rate um = new Rate();

            um.UserId = UserId;
            um.JobId = JobId;
            um.Rate1 = Math.Round(Rate);
            um.SeekerId = SeekerId;
            um.CreateDate = DateTime.Now;
            DB.Rates.InsertOnSubmit(um);

            DB.SubmitChanges();

            var Jobdetail = (from a in DB.Jobs where a.Id == JobId select a).FirstOrDefault();

            var receiverdata1 = (from um1 in DB.UserMasters
                                 where um1.Id == UserId
                                 select new ReceiverData
                                 {
                                     UserEmail = um1.UserEmail,
                                     DeviceType = um1.DeviceType,
                                     DeviceId = um1.DeviceId,
                                     FullName = um1.FirstName,
                                     FirstName = um1.FirstName,
                                     UserName = um1.UserName

                                 }).FirstOrDefault();
            var senderdata1 = (from u in DB.UserMasters
                               where u.Id == SeekerId
                               select new SenderData
                               {

                                   SenderFullName = u.FirstName + " " + u.LastName,
                                   UserName = u.UserName,
                                   SenderProfilePicture = path + u.ProfilePicture
                               }).FirstOrDefault();


            string applink1 = "";
            if (Jobdetail.Applink == null)
            {
                applink1 = "";
            }
            else
            {
                applink1 = Jobdetail.Applink + "?Poster";
            }
            string NotificationType = "Job Completed";
            if (receiverdata1.DeviceType.ToUpper() == "ANDROID")
            {
                SendNotification(receiverdata1.DeviceId, SeekerId.ToString(), UserId.ToString(), senderdata1.SenderFullName, receiverdata1.FullName + ", your job has been completed.", JobId, NotificationType, senderdata1.SenderFullName, senderdata1.SenderProfilePicture, "");
            }
            else
            {
                string otherparam = "\"FromUserId\":\"" + SeekerId + "\",\"toUserId\":\"" + UserId + "\",\"fullName\":\"" + senderdata1.SenderFullName + "\",\"JobId\":\"" + JobId + "\",\"NotificationType\":\"" + NotificationType + "\",\"SenderFullName\":\"" + senderdata1.SenderFullName + "\",\"SenderProfilePicture\":\"" + senderdata1.SenderProfilePicture + "\" ";
                string message = receiverdata1.FullName + ", your job has been completed.";
                SendIhpone(receiverdata1.DeviceId, otherparam, message);
            }

            string str = "<div><h2 style='color:blue !important'><img src='http://13.72.77.167:8085/ApplicationImages/LogoAzzida.png'/></h2></br>";
            //  str = str + "<h5 style='color:blue !important;'>" + "Start Getting Stuff Done!" + " </h5></br>";
            str = str + "<p style='color:black !important;'>Dear " + receiverdata1.FullName + ", </p></br>";
            str = str + "<p style='color:black !important;'>Your Job Performer, " + senderdata1.SenderFullName + " has indicated that " + '"' + Jobdetail.JobTitle + '"' + " has been completed. Click here to confirm " + "<a href='" + applink1 + "' style='color:blue !important;'>" + applink1 + "</a> </p></br>";
            // str = str + "<p><a href='https://www.facebook.com/azzidajobs/' style='color:black !important;'>Follow us on Facebook</a></p></br>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>The Team At Azzida</p>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>Email: <a href='mailto:support@azzida.com' style='color:blue !important;'>support@azzida.com</a></p>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>AppLink: <a href='https://play.google.com/store/apps/details?id=com.azzida' style='color:blue !important;'>https://play.google.com/store/apps/details?id=com.azzida</a></p>";
            str = str + "<p style='margin-block: 0px !important;'><a href='https://www.instagram.com/azzida_app' style='color:blue !important;'>Follow us on Instagram</a></p>";
            str = str + "<p style='color:black !important; margin-block: 0px !important;'>Web: <a href='http://www.azzida.com/' style='color:blue !important;'>www.Azzida.com</a></p>";
            //str = str + "<p style='color:black !important;margin-block: 0px !important;'>Email: <a href='mailto:support@azzida.com' style='color:blue !important;'>support@azzida.com</a></p>";
            str = str + "</div>";

            SendEMail("noreply@azzida.com", receiverdata1.UserEmail, NotificationType, str);



            return um;
        }

        //public AndroidFCMPushNotificationStatus SendNotification(string deviceId, string from, string to, string fullname, string msg, int JobId, string NotificationType)
        //{
        //    AndroidFCMPushNotificationStatus result = new AndroidFCMPushNotificationStatus();

        //    try
        //    {
        //        string applicationID = "AAAAzCwSTjw:APA91bHgcn0SVAB2t5WLVQpl7yQHUbvv_tYJMe4jw68egvE_cnVN6sWfTTHTOymKvylv2M_cHcJTLsmHRqcZyt5j2YYAy7Zv2Idb8StHH1Mfz8ujoDfVREnn_llt0_JpwRtbXfVIt4Zq";
        //        string senderId = "876912725564";



        //        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //        tRequest.Method = "post";
        //        tRequest.ContentType = "application/json";

        //        var data = new
        //        {
        //            to = deviceId,

        //            data = new
        //            {
        //                title = "Azzida",
        //                message = msg,
        //                priority = "high",
        //                FromUserId = from,
        //                ToUserId = to,
        //                FromUserName = fullname,
        //                NotificationType= NotificationType,
        //                // AvailabiltyTimeFrom = AvailabiltyTimeFrom,
        //                // AvailabiltyTimeTo = AvailabiltyTimeTo,
        //                JobId = JobId

        //            }

        //        };
        //        //var serializer = new Newtonsoft.Json.JsonSerializer();
        //        //var json = "{\"to\":\"" + deviceId + "\",\"data\":{\"body\":\"" + message + "\",\"title\":\"notification\",\"sound\":\"Enabled\"}}";
        //        var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
        //        Byte[] byteArray = Encoding.UTF8.GetBytes(json);
        //        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
        //        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
        //        tRequest.ContentLength = byteArray.Length;
        //        using (Stream dataStream = tRequest.GetRequestStream())
        //        {
        //            dataStream.Write(byteArray, 0, byteArray.Length);
        //            using (WebResponse tResponse = tRequest.GetResponse())
        //            {
        //                using (Stream dataStreamResponse = tResponse.GetResponseStream())
        //                {
        //                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //                    {
        //                        String sResponseFromServer = tReader.ReadToEnd();
        //                        string str = sResponseFromServer;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = ex.Message;
        //    }
        //    return result;
        //}

        //suspend user
        public UserMaster UserActiveDeactive(int Id)
        {
            UserMaster um = new UserMaster();

            um = (from a in DB.UserMasters where a.Id == Id select a).FirstOrDefault();
            if (um.IsActive == true)
            {
                um.IsActive = false;
            }
            //else
            //{
            //    um.IsActive = true;
            //}



            DB.SubmitChanges();
            return um;
        }

        //public void DeleteUser(int Id)
        //{
        //    var data = (from a in DB.UserMasters where a.Id == Id select a).FirstOrDefault();
        //    DB.UserMasters.DeleteOnSubmit(data);
        //    DB.SubmitChanges();
        //}

        public GetLoginDetail UserLogin(string UserName, string UserPassword, string deviceId, string devicetype)
        {
            var data = (from a in DB.UserMasters
                        where (a.UserName == UserName || a.UserEmail == UserName)
                            && a.UserPassword == UserPassword
                        select a).FirstOrDefault();
            if (data != null)
            {
                data.DeviceId = deviceId;
                data.DeviceType = devicetype;
                DB.SubmitChanges();


            }
            var data1 = (from b in DB.UserMasters
                         where (b.UserName == UserName || b.UserEmail == UserName)
                        && b.UserPassword == UserPassword
                         select new GetLoginDetail
                         {
                             Id = b.Id,
                             FirstName = b.FirstName,
                             LastName = b.LastName,
                             ProfilePicture = b.ProfilePicture,
                             RoleId = b.RoleId,
                             Skills = b.Skills,
                             UserEmail = b.UserEmail,
                             UserPassword = b.UserPassword,
                             TokenId = b.TokenId,
                             UserName = b.UserName,
                             VerifiedId = b.VerifiedId,
                             Provider = b.Provider,
                             JobType = b.JobType,
                             IsVerified = b.IsVerified,
                             IsActive = b.IsActive,
                             GoogleEmail = b.GoogleEmail,
                             FaceBookEmail = b.FaceBookEmail,
                             EmailType = b.EmailType,
                             DeviceType = b.DeviceType,
                             RefCode = b.RefCode,
                             StripeAccId = b.StripeAccId,
                             AzzidaVerified = Convert.ToString(b.AzzidaVerified) == "true" ? true : false,
                             //  Balance = balance == null ? 0 : balance,
                             DeviceId = b.DeviceId,
                             CreatedDate = b.CreatedDate,
                             receivedAmount = b.UserReceivedAmount ?? 0,

                             //  UserRatingAvg = Math.Round(avg),
                         }).FirstOrDefault();

            return data1;
        }
        public UserMaster LoginAdmin(string UserName, string UserPassword)
        {
            var data = (from a in DB.UserMasters where a.UserName == UserName && a.UserPassword == UserPassword select a).FirstOrDefault();
            return data;
        }

        public UserMaster IsEmailExist(string UserEmail)
        {
            return (from a in DB.UserMasters where a.UserEmail == UserEmail select a).FirstOrDefault();
        }
        public UserMaster IsUserNameExist(string UserName)
        {
            return (from a in DB.UserMasters where a.UserName == UserName select a).FirstOrDefault();
        }

        public UserMaster IsRefCodeExist(string ReferalCode)
        {
            var data = (from a in DB.UserMasters where a.RefCode == ReferalCode select a).FirstOrDefault();
            return data;
        }
        public UserMaster ForgotPassword(string email)
        {
            string password = Guid.NewGuid().ToString("n").Substring(0, 6).ToUpper();
            var data = (from a in DB.UserMasters where a.UserEmail == email select a).FirstOrDefault();
            data.UserPassword = password;
            DB.SubmitChanges();
            //string str = "";
            string str = "<div><p style='color:black !important'>" + "Hi " + data.UserName.Trim() + "," + "</p>";

            str = str + "<p style='color:black !important'>" + "Your new password is " + password + "." + "</p>";




            SendEMail("noreply@azzida.com", email, "Forgot Password", str);

            return data;
        }


        public static void SendEMail(string FromMail, string toMail, string subject, string body)
        {
            try
            {
                //MailMessage mailMessage = new mailmessage("no-reply@listyapp.com", Email);
                MailMessage mailMessage = new MailMessage("noreply@azzida.com", toMail);

                mailMessage.Bcc.Add("sntalior6@gmail.com");
                //mailmessage.bcc.add("mittal181998@gmail.com");
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();

                smtpClient.Credentials = new System.Net.NetworkCredential("noreply@azzida.com", @"a84#36_K21@9");
                //smtpClient.Credentials = new system.net.networkcredential("no-reply@listyapp.com", "#YXYP^$K1F@6K6372ER");
                //  smtpClient.Credentials = new System.Net.NetworkCredential("help@logictrixtech.com", @"5*b,o0t^3Prx");
                //smtpClient.Timeout = 1000000;
                smtpClient.Port = 587;
                smtpClient.Host = "mail.azzida.com";
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n", ex.Message);
            }


        }


        public List<UserMaster> GetUserList()
        {
            var data = (from a in DB.UserMasters select a).ToList();
            return data;
        }

        public UserMaster GetUserById(int Id)
        {
            var data = (from a in DB.UserMasters where a.Id == Id select a).FirstOrDefault();
            return data;
        }

        public void DeleteUserById(int Id)
        {
            var data = (from a in DB.UserMasters where a.Id == Id select a).FirstOrDefault();
            var jobdata = (from j in DB.Jobs where j.UserId == Id select j).ToList();
            if (jobdata.Count > 0)
            {
                DB.Jobs.DeleteAllOnSubmit(jobdata);
                DB.SubmitChanges();
            }

            DB.UserMasters.DeleteOnSubmit(data);
            DB.SubmitChanges();
        }
        //  jobfee

        public JobFee GetJobFee()
        {
            var data = (from a in DB.JobFees select a).FirstOrDefault();
            return data;
        }
        public JobFee SaveJobFee(int Id, string feeforseeker, string feeforlister, string Backgroundcheck, string canceljobfee)
        {
            JobFee jf = new JobFee();
            if (Id > 0)
            {
                jf = (from a in DB.JobFees where a.Id == Id select a).FirstOrDefault();
                jf.JobSeekerFee = Convert.ToDecimal(feeforseeker);
                jf.JobListerFee = Convert.ToDecimal(feeforlister);
                jf.CancelJobFee = Convert.ToDecimal(canceljobfee);
                jf.BackgroundCheck = Convert.ToDecimal(Backgroundcheck);
            }
            else
            {
                jf.JobSeekerFee = Convert.ToDecimal(feeforseeker);
                jf.JobListerFee = Convert.ToDecimal(feeforlister);
                jf.CancelJobFee = Convert.ToDecimal(canceljobfee);
                jf.BackgroundCheck = Convert.ToDecimal(Backgroundcheck);
                jf.CreateDate = System.DateTime.Now;
                DB.JobFees.InsertOnSubmit(jf);

            }
            DB.SubmitChanges();
            return jf;
        }


        //586069

        public RecentActivity GetRecentActivity(int UserId)
        {
            RecentActivity ra = new RecentActivity();
            var PostedJob = (from a in DB.Jobs
                             where a.UserId == UserId && a.IscompleteUser.ToString() == "false"
                           && a.IsCancel.ToString() == "false"
                             select new postJobs
                             {
                                 Id = a.Id,
                                 JobTitle = a.JobTitle,
                                 JobCategory = a.JobCategory,
                                 JobDescription = a.JobDescription,
                                 Latitude = a.Latitude,
                                 Longitude = a.Longitude,
                                 Amount = a.Amount,
                                 CreatedDate = a.CreatedDate,
                                 Location = a.Location,
                                 Howlong = a.HowLong,
                                 UserId = a.UserId,
                                 FromDate = a.FromDate,
                             }).OrderByDescending(x => x.CreatedDate).ToList();
            var AppliedJob = (from b in DB.ApplicationStatus
                              join j in DB.Jobs on b.JobId equals j.Id

                              where b.SeekerId == UserId && b.NotSelected.ToString() == "false" && j.IscompleteUser.ToString() == "false"
                              select new appliedJob
                              {
                                  Id = j.Id,
                                  ApplierId = b.SeekerId,
                                  JobTitle = j.JobTitle,
                                  JobCategory = j.JobCategory,
                                  JobDescription = j.JobDescription,
                                  Latitude = j.Latitude,
                                  Longitude = j.Longitude,
                                  Amount = j.Amount,
                                  CreatedDate = j.CreatedDate,
                                  Location = j.Location,
                                  Howlong = j.HowLong,
                                  UserId = j.UserId,
                                  FromDate = j.FromDate,
                              }).OrderByDescending(x => x.CreatedDate).ToList();
            ra.post = PostedJob;
            ra.applied = AppliedJob;
            //ra. = PostedJob;
            return ra;
        }

        //  job repost
        public Job RepostJob(int JobId, int UserId, string FromDate)
        {
            var data = (from a in DB.Jobs where a.Id == JobId && a.UserId == UserId select a).FirstOrDefault();
            if (data != null)
            {
                var appstatus = (from b in DB.ApplicationStatus where b.JobId == JobId select b).ToList();
                if (appstatus.Count > 0)
                {
                    DB.ApplicationStatus.DeleteAllOnSubmit(appstatus);
                    DB.SubmitChanges();

                }
                var AdminCharges = (from jf in DB.JobFees select jf).FirstOrDefault();
                data.FromDate = FromDate;
                if (AdminCharges != null)
                {
                    decimal jobAmountWithAdminCharges = (data.Amount) * (AdminCharges.JobListerFee / 100) + (data.Amount) ?? 0;

                    data.AmountWithAdminCharges = jobAmountWithAdminCharges;
                }
                data.AssignSeekerId = 0;
                data.IsCancel = false;
                data.IsComplete = false;
                data.IscompleteUser = false;
                data.CancelReason = "";
                data.CreatedDate = System.DateTime.Now;
                DB.SubmitChanges();


            }
            return data;

        }

        //offered job by lister
        public List<GetUserJob> OfferedJob(int UserId)
        {
            var data = (from a in DB.Jobs
                        where a.UserId == UserId && a.IsComplete.ToString() == "false"
                        select new GetUserJob
                        {
                            Id = a.Id,
                            JobTitle = a.JobTitle,
                            JobCategory = a.JobCategory,
                            JobDescription = a.JobDescription,
                            JobPicture = path + a.JobPicture,
                            Location = a.Location,
                            Longitude = a.Longitude,
                            Latitude = a.Latitude,
                            UserId = a.UserId,
                            IsComplete = a.IsComplete,
                            HowLong = a.HowLong,
                            FromDate = a.FromDate,
                            CompletedDate = a.CompletedDate,
                            Amount = a.Amount

                        }).ToList();
            GetUserJob userjob = new GetUserJob();
            List<GetUserJob> offerjobs = new List<GetUserJob>();

            foreach (var i in data)
            {
                userjob = new GetUserJob();
                var IsofferedJob = (from o in DB.ApplicationStatus
                                    where o.JobId == i.Id
                                    && o.ListerId == i.UserId && o.IsAcceptedByLister.ToString() == "true"
                                    select o).FirstOrDefault();
                if (IsofferedJob == null)
                {


                    userjob.Id = i.Id;
                    userjob.UserId = i.UserId ?? 0;
                    userjob.JobTitle = i.JobTitle;
                    userjob.JobPicture = i.JobPicture;
                    userjob.Latitude = i.Latitude;
                    userjob.Longitude = i.Longitude;
                    userjob.Location = i.Location;
                    userjob.HowLong = i.HowLong;
                    userjob.FromDate = i.FromDate;
                    userjob.CompletedDate = i.CompletedDate;

                    userjob.IsComplete = i.IsComplete;

                    userjob.JobDescription = i.JobDescription;
                    offerjobs.Add(userjob);
                }
            }
            return offerjobs;
        }

        //Active job of lister 
        public List<Job> ActiveListerJobs(int UserId)
        {
            var data = (from a in DB.Jobs
                        where a.UserId == UserId
      && a.AssignSeekerId != null
      && a.IsComplete.ToString() == "false"
                        select a).ToList();
            return data;
        }
        // Completed by Lister
        public List<Job> ListerCompletedJob(int UserId)
        {
            var data = (from a in DB.Jobs
                        where a.UserId == UserId &&
                        a.IsComplete.ToString() == "true" &&
                        a.IscompleteUser.ToString() == "true"
                        && a.IsCancel.ToString() == "false"

                        select a).ToList();
            return data;
        }
        //completed by Seeker
        public List<Job> SeekerCompletedJob(int UserId)
        {
            var data = (from a in DB.Jobs
                        where a.AssignSeekerId == UserId &&
                        a.IsComplete.ToString() == "true" &&
                        a.IscompleteUser.ToString() == "true" &&
                        a.IsCancel.ToString() == "false"

                        select a).ToList();
            return data;
        }





        //pending in Lister

        public List<GetUserJob> ListerPendingJob(int UserId)
        {
            var data = (from a in DB.Jobs
                        where a.UserId == UserId && a.IsComplete.ToString() == "false"
                        select new GetUserJob
                        {
                            Id = a.Id,
                            JobTitle = a.JobTitle,
                            JobCategory = a.JobCategory,
                            JobDescription = a.JobDescription,
                            JobPicture = path + a.JobPicture,
                            Location = a.Location,
                            Longitude = a.Longitude,
                            Latitude = a.Latitude,
                            UserId = a.UserId,
                            IsComplete = a.IsComplete,
                            HowLong = a.HowLong,
                            FromDate = a.FromDate,
                            CompletedDate = a.CompletedDate,
                            Amount = a.Amount

                        }).ToList();
            GetUserJob userjob = new GetUserJob();
            List<GetUserJob> pendingjobs = new List<GetUserJob>();
            foreach (var i in data)
            {
                userjob = new GetUserJob();
                var IsPendingJob = (from o in DB.ApplicationStatus
                                    where o.JobId == i.Id
                                    && o.ListerId == i.UserId
                                    //&& o.IsAcceptedByLister.ToString() == "true" &&
                                    //o.IsAccteptedBySeeker.ToString() == "false"
                                    select o).ToList();
                if (IsPendingJob != null)
                {
                    userjob.Id = i.Id;
                    userjob.UserId = i.UserId ?? 0;
                    userjob.JobTitle = i.JobTitle;
                    userjob.JobPicture = i.JobPicture;
                    userjob.Latitude = i.Latitude;
                    userjob.Longitude = i.Longitude;
                    userjob.Location = i.Location;
                    userjob.HowLong = i.HowLong;
                    userjob.FromDate = i.FromDate;
                    userjob.CompletedDate = i.CompletedDate;

                    userjob.IsComplete = i.IsComplete;

                    userjob.JobDescription = i.JobDescription;


                    pendingjobs.Add(userjob);
                }
            }
            return pendingjobs;
        }
        // do cancel job 

        public void CancelJobById(int JobId, string Reason, int UserId)
        {
            var data = (from a in DB.Jobs where a.Id == JobId select a).FirstOrDefault();
            var fee = (from b in DB.JobFees select b).FirstOrDefault();
            var jobstatus = (from c in DB.ApplicationStatus where c.JobId == JobId select c).ToList();
            //string startdatetime = data.FromDate;

            //var date = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(startdatetime));
            //var date2 = System.DateTime.Now;
            //var hours = (date2 - date).TotalHours;
            //string hoursdiff = hours.ToString();



            if (data.UserId == UserId)
            {

                data.IsCancel = true;
                data.CancelReason = Reason;
                data.CancelDate = System.DateTime.Now;
                DB.SubmitChanges();


            }
            else
            {
                if (data.AssignSeekerId == UserId)
                {
                    DB.ApplicationStatus.DeleteAllOnSubmit(jobstatus);
                    DB.SubmitChanges();


                    data.IsCancelSeeker = true;
                    data.SeekerCancelReason = Reason;
                    data.AssignSeekerId = 0;
                    DB.SubmitChanges();

                }
            }

        }

        public List<JobCancel> ListerJobCancelledList(int UserId)
        {
            List<JobCancel> j = new List<JobCancel>();
            //JobCancel jc = new JobCancel();
            double milos = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            var data = (from a in DB.Jobs where a.UserId == UserId && (a.AssignSeekerId == null || a.AssignSeekerId == 0) select a).ToList();
            //   comment on 14 sept
            //var data = (from a in DB.Jobs where a.UserId == UserId && (a.IsCancel.ToString() == "true" || a.AssignSeekerId == null) select a).ToList();
            //  var data2 = (from a in DB.Jobs where a.UserId == UserId && a.IsCancel.ToString()=="true" select a).ToList();
            foreach (var s in data)
            {
                if (s.IsCancel == true)
                {
                    JobCancel jc = new JobCancel();
                    jc.JobId = s.Id;
                    jc.JobTitle = s.JobTitle;
                    jc.JobCategory = s.JobCategory;
                    jc.JobDescription = s.JobDescription;
                    jc.Latitude = s.Latitude;
                    jc.Longitude = s.Longitude;
                    jc.Location = s.Location;
                    jc.FromDate = s.FromDate;
                    jc.HowLong = s.HowLong;
                    jc.CancelDate = s.CancelDate;
                    jc.Amount = s.Amount;
                    jc.AssignSeekerId = s.AssignSeekerId;
                    jc.Status = "Cancelled";
                    j.Add(jc);
                }
                else if (Convert.ToDouble(s.FromDate) < milos)
                {
                    JobCancel jc = new JobCancel();
                    jc.JobId = s.Id;
                    jc.JobTitle = s.JobTitle;
                    jc.JobCategory = s.JobCategory;
                    jc.JobDescription = s.JobDescription;
                    jc.Latitude = s.Latitude;
                    jc.Longitude = s.Longitude;
                    jc.Location = s.Location;
                    jc.FromDate = s.FromDate;
                    jc.HowLong = s.HowLong;
                    jc.Amount = s.Amount;
                    jc.Status = "Expired";
                    j.Add(jc);
                }
                else
                {

                }

            }
            //var data1 = (from x in DB.Jobs where x.AssignSeekerId == null && x.UserId==UserId  select x).ToList();
            //foreach (var b in data)
            //{
            //    JobCancel jc = new JobCancel();
            //    jc.JobId = b.Id;
            //    jc.JobTitle = b.JobTitle;
            //    jc.JobCategory = b.JobCategory;
            //    jc.JobDescription = b.JobDescription;
            //    jc.Latitude = b.Latitude;
            //    jc.Longitude = b.Longitude;
            //    jc.Location = b.Location;
            //    jc.FromDate = b.FromDate;
            //    jc.HowLong = b.HowLong;
            //    jc.Amount = b.Amount;
            //    jc.Status = "Cancelled";
            //    j.Add(jc);
            //}

            return j;
        }


        public List<JobCancel> SeekerJobCancelledList(int UserId)
        {
            List<JobCancel> jc = new List<JobCancel>();
            // JobCancel j = new JobCancel();
            double milos = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            var appstatus = (from b in DB.ApplicationStatus where b.SeekerId == UserId select b).ToList();
            foreach (var t in appstatus)
            {
                var jobdata = (from a in DB.Jobs where a.Id == t.JobId select a).FirstOrDefault();
                if (jobdata != null)
                {
                    //var userJobStatus = appstatus.Where(x.JobId == jobdata.Id && x.ListerId == jobdata.UserId && x.NotSelected == true).ToList();
                    if (t.NotSelected == true)
                    {
                        JobCancel j = new JobCancel();
                        j.JobId = jobdata.Id;
                        j.JobTitle = jobdata.JobTitle;
                        j.JobCategory = jobdata.JobCategory;
                        j.JobDescription = jobdata.JobDescription;
                        j.HowLong = jobdata.HowLong;
                        j.FromDate = jobdata.FromDate;
                        j.Latitude = jobdata.Latitude;
                        j.Longitude = jobdata.Longitude;
                        j.Location = jobdata.Location;
                        j.Amount = jobdata.Amount;
                        j.Status = "Not Selected";
                        jc.Add(j);
                    }
                    else
                    {
                        if (jobdata.IsCancel == true)
                        {
                            JobCancel j = new JobCancel();
                            j.JobId = jobdata.Id;
                            j.JobTitle = jobdata.JobTitle;
                            j.JobCategory = jobdata.JobCategory;
                            j.JobDescription = jobdata.JobDescription;
                            j.HowLong = jobdata.HowLong;
                            j.FromDate = jobdata.FromDate;
                            j.Latitude = jobdata.Latitude;
                            j.Longitude = jobdata.Longitude;
                            j.Location = jobdata.Location;
                            j.Amount = jobdata.Amount;
                            j.CancelDate = jobdata.CancelDate;
                            j.Status = "Cancelled";
                            jc.Add(j);
                        }


                        else if (Convert.ToDouble(jobdata.FromDate) < milos)
                        {
                            JobCancel j = new JobCancel();
                            j.JobId = jobdata.Id;
                            j.JobTitle = jobdata.JobTitle;
                            j.JobCategory = jobdata.JobCategory;
                            j.JobDescription = jobdata.JobDescription;
                            j.HowLong = jobdata.HowLong;
                            j.FromDate = jobdata.FromDate;
                            j.Latitude = jobdata.Latitude;
                            j.Longitude = jobdata.Longitude;
                            j.Location = jobdata.Location;
                            j.Amount = jobdata.Amount;
                            j.Status = "Expired";
                            jc.Add(j);
                        }
                        else
                        {

                        }
                    }
                }

            }
            return jc;
        }
        public List<InProgressJob> ListerJobInProgress(int UserId)
        {
            double milos = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            List<InProgressJob> progressjobs = new List<InProgressJob>();
            InProgressJob jobdetail = new InProgressJob();
            var data = (from a in DB.Jobs where a.UserId == UserId && a.IscompleteUser.ToString() != "true" && (a.IsCancel.ToString() != "true" && Convert.ToDouble(a.FromDate) > milos) select a).ToList();

            var application = (from b in DB.ApplicationStatus where b.ListerId == UserId select b).ToList();
            foreach (var s in data)
            {
                jobdetail = new InProgressJob();
                var Applicant = (from a in DB.ApplicationStatus where a.JobId == s.Id select a).ToList();
                jobdetail.JobId = s.Id;
                jobdetail.ListerId = UserId;
                //jobdetail.ListerName = listerDetail.FirstName + " " + listerDetail.LastName;
                //jobdetail.ListerProfilePicture = path + listerDetail.ProfilePicture;
                //jobdetail.ListerCompleteJob = listercompleteJob.Count();
                jobdetail.JobTitle = s.JobTitle;
                jobdetail.Latitude = s.Latitude;
                jobdetail.JobCategory = s.JobCategory;
                jobdetail.JobDescription = s.JobDescription;
                jobdetail.JobPicture = path + s.JobPicture;
                jobdetail.FromDate = s.FromDate;
                jobdetail.HowLong = s.HowLong;
                jobdetail.Amount = s.Amount;
                jobdetail.Location = s.Location;
                jobdetail.Latitude = s.Latitude;
                jobdetail.Longitude = s.Longitude;
                jobdetail.CreatedDate = s.CreatedDate;
                jobdetail.IsComplete = s.IsComplete == null ? false : s.IsComplete;
                jobdetail.ApplicantCount = Applicant.Count();

                if (application.Count > 0)
                {

                    var d = application.Where(x => x.JobId == s.Id && x.IsApply == true &&
                x.IsAcceptedByLister == true).FirstOrDefault();
                    if (d != null)
                    {
                        jobdetail.IsApply = true;
                        jobdetail.ApplicationAccepted = true;
                        if (d.IsAccteptedBySeeker == true)
                        {
                            jobdetail.OfferAccepted = true;
                        }
                    }
                    else
                    {
                        if (Applicant.Count > 0)
                        {
                            jobdetail.IsApply = true;
                            jobdetail.ApplicationAccepted = false;
                            jobdetail.OfferAccepted = false;
                        }
                        else
                        {
                            jobdetail.IsApply = false;
                            jobdetail.ApplicationAccepted = false;
                            jobdetail.OfferAccepted = false;
                        }

                    }
                    //}
                    //foreach (var t in application)
                    //{
                    //    if (t.JobId == s.Id && t.IsApply == true)
                    //    {
                    //        jobdetail.IsApply = true;

                    //        if (t.IsAcceptedByLister == true)
                    //        {
                    //            jobdetail.ApplicationAccepted = true;
                    //        }
                    //        else
                    //        {
                    //            jobdetail.ApplicationAccepted = false;
                    //        }
                    //        if (t.IsAccteptedBySeeker == true)
                    //        {
                    //            jobdetail.OfferAccepted = true;
                    //        }
                    //        else
                    //        {
                    //            jobdetail.OfferAccepted = false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        jobdetail.IsApply = false;

                    //    }

                    //}

                }
                else
                {
                    jobdetail.IsApply = false;
                    jobdetail.ApplicationAccepted = false;
                    jobdetail.OfferAccepted = false;

                }
                progressjobs.Add(jobdetail);


            }
            progressjobs = progressjobs.OrderByDescending(x => x.CreatedDate).ToList();
            return progressjobs;
        }

        public List<InProgressJob> SeekerJobInProgress(int UserId)
        {
            double milos = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            List<InProgressJob> progressjobs = new List<InProgressJob>();
            InProgressJob jobdetail = new InProgressJob();
            var data = (from a in DB.Jobs where a.UserId != UserId && a.IscompleteUser.ToString() == "false" && a.IsCancel.ToString() == "false" && a.IsCancelSeeker.ToString() == "false" && Convert.ToDouble(a.FromDate) > milos select a).ToList();
            //  var listerDetail = (from um in DB.UserMasters where um.Id == job.UserId select um).FirstOrDefault();
            var application = (from b in DB.ApplicationStatus where b.SeekerId == UserId && b.NotSelected.ToString() == "false" select b).ToList();
            foreach (var s in application)
            {


                var data1 = data.Where(x => x.Id == s.JobId).ToList();
                foreach (var t in data1)
                {
                    jobdetail = new InProgressJob();
                    jobdetail.JobId = t.Id;
                    jobdetail.ListerId = t.UserId;

                    jobdetail.JobTitle = t.JobTitle;
                    jobdetail.Latitude = t.Latitude;
                    jobdetail.JobCategory = t.JobCategory;
                    jobdetail.JobDescription = t.JobDescription;
                    jobdetail.JobPicture = path + t.JobPicture;
                    jobdetail.FromDate = t.FromDate;
                    jobdetail.HowLong = t.HowLong;
                    jobdetail.Amount = t.Amount;
                    jobdetail.Location = t.Location;
                    jobdetail.Latitude = t.Latitude;
                    jobdetail.Longitude = t.Longitude;
                    jobdetail.IsComplete = t.IsComplete == null ? false : t.IsComplete;

                    if (s.JobId == t.Id && s.IsApply == true && s.SeekerId != t.UserId)
                    {
                        jobdetail.IsApply = true;
                        if (s.IsAcceptedByLister == true)
                        {
                            jobdetail.ApplicationAccepted = true;
                        }
                        else
                        {
                            jobdetail.ApplicationAccepted = false;
                        }
                        if (s.IsAccteptedBySeeker == true)
                        {
                            jobdetail.OfferAccepted = true;
                        }
                        else
                        {
                            jobdetail.OfferAccepted = false;
                        }
                        jobdetail.ApplicationModifyDate = s.ModifyDate;
                    }
                    else
                    {
                        jobdetail.IsApply = false;
                        jobdetail.ApplicationAccepted = false;
                        jobdetail.OfferAccepted = false;
                        jobdetail.ApplicationModifyDate = t.CreatedDate;

                    }

                    progressjobs.Add(jobdetail);
                    progressjobs = progressjobs.OrderByDescending(x => x.ApplicationModifyDate).ToList();
                }

            }



            //}
            return progressjobs;
        }
        public JobDetail GetJobDetail(int JobId, int UserId)
        {
            double milos = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            var Applicant = (from a in DB.ApplicationStatus where a.JobId == JobId select a).ToList();
            var getpaymentId = (from p in DB.Payments where p.JobId == JobId && p.PaymentType.ToLower() == "payment" select p).FirstOrDefault();
            var gettippaymentid = (from p in DB.Tips where p.JobId == JobId select p).FirstOrDefault();
            // && p.PaymentType.ToLower() == "tip"
            var JobImage = (from s in DB.JobImages
                            where s.JobId == JobId
                            select new JobImageList
                            {
                                Id = s.Id,
                                JobId = s.JobId,
                                UserId = s.UserId,
                                ImageName = path + s.ImageName

                            }).ToList();
            var job = (from j in DB.Jobs
                       where j.Id == JobId
                       select j).FirstOrDefault();
            var listerDetail = (from um in DB.UserMasters where um.Id == job.UserId select um).FirstOrDefault();
            var listercompleteJob = (from s in DB.Jobs where s.AssignSeekerId == job.UserId && s.IsComplete.ToString() == "true" select s).ToList();
            JobDetail jobdetail = new JobDetail();
            if (job.UserId == UserId)
            {
                //Lister
                ApplicationStatus apps = new ApplicationStatus();
                ApplicationStatus app = new ApplicationStatus();
                app = (from n in DB.ApplicationStatus where n.ListerId == UserId && n.JobId == JobId && n.IsApply.ToString() == "true" select n).FirstOrDefault();
                apps = (from n in DB.ApplicationStatus where n.ListerId == UserId && n.JobId == JobId && n.IsApply.ToString() == "true" && n.IsAcceptedByLister.ToString() == "true" select n).FirstOrDefault();


                jobdetail.JobId = JobId;
                jobdetail.ListerId = UserId;
                jobdetail.ListerName = listerDetail.FirstName + " " + listerDetail.LastName;
                jobdetail.ListerProfilePicture = path + listerDetail.ProfilePicture;
                jobdetail.ListerCompleteJob = listercompleteJob.Count();
                jobdetail.JobTitle = job.JobTitle;
                jobdetail.Latitude = job.Latitude;
                jobdetail.JobCategory = job.JobCategory;
                jobdetail.JobDescription = job.JobDescription;
                jobdetail.JobPicture = path + job.JobPicture;
                jobdetail.FromDate = job.FromDate;
                jobdetail.HowLong = job.HowLong;
                jobdetail.CompletionDate = job.CompletedDate;
                jobdetail.Amount = job.Amount == null ? 0 : job.Amount;
                jobdetail.AmountWithAdminCharges = job.AmountWithAdminCharges == null ? 0 : job.AmountWithAdminCharges;
                jobdetail.Location = job.Location;
                jobdetail.Latitude = job.Latitude;
                jobdetail.Longitude = job.Longitude;

                jobdetail.ApplicantCount = Applicant.Count();
                var AzzidaVerify = (from x in DB.UserMasters where x.Id == jobdetail.ListerId select x.AzzidaVerified).FirstOrDefault();
                if (AzzidaVerify != null)
                {
                    jobdetail.AzzidaVarified = AzzidaVerify;
                }
                else
                {
                    jobdetail.AzzidaVarified = false;
                }
                if (gettippaymentid != null)
                {
                    jobdetail.TipId = gettippaymentid.Id;
                }
                else
                {
                    jobdetail.TipId = 0;
                }

                if (getpaymentId != null)
                {
                    jobdetail.PymntId = getpaymentId.Id;
                }
                else
                {
                    jobdetail.PymntId = 0;
                }
                if (job.IsCancel == true)
                {
                    jobdetail.Status = "Cancelled";
                }
                else if (Convert.ToDouble(job.FromDate) < milos)
                {
                    jobdetail.Status = "Expired";
                }
                else
                {
                    jobdetail.Status = "";
                }
                if (job.IsComplete == false)
                {
                    jobdetail.IsComplete = false;
                }
                if (job.IsComplete == null)
                {
                    jobdetail.IsComplete = false;
                }
                if (job.IsComplete == true)
                {
                    jobdetail.IsComplete = true;
                }

                jobdetail.IsJobComplete = job.IscompleteUser == null ? false : job.IscompleteUser;
                if (app != null)//Applicant.Count > 0
                {
                    jobdetail.IsApply = true;
                }
                else
                {
                    jobdetail.IsApply = false;
                }
                if (apps != null)
                {
                    //jobdetail.IsApply = apps.IsApply;
                    jobdetail.ApplicationAccepted = true;
                    jobdetail.SeekerId = apps.SeekerId;
                    var Seekerdetails = (from um in DB.UserMasters where um.Id == apps.SeekerId select um).FirstOrDefault();
                    if (Seekerdetails != null)
                    {
                        jobdetail.SeekerName = Seekerdetails.UserName;
                        jobdetail.Seekerimage = path + Seekerdetails.ProfilePicture;
                    }
                    else
                    {
                        jobdetail.SeekerName = "";
                        jobdetail.Seekerimage = "";
                    }
                    if (apps.IsAccteptedBySeeker == true)
                    {
                        jobdetail.OfferAccepted = true;
                    }
                    else
                    {
                        jobdetail.OfferAccepted = false;
                    }



                }
                else
                {
                    //jobdetail.IsApply = true;
                    jobdetail.ApplicationAccepted = false;
                    jobdetail.OfferAccepted = false;
                }

                jobdetail.imageList = JobImage;

            }
            else
            {
                //seeker
                // List<ApplicationStatus> apps = new List<ApplicationStatus>();

                ApplicationStatus apps = new ApplicationStatus();
                ApplicationStatus app = new ApplicationStatus();
                app = (from n in DB.ApplicationStatus where n.SeekerId == UserId && n.JobId == JobId && n.IsApply.ToString() == "true" select n).FirstOrDefault();
                //var data = (from a in DB.ApplicationStatus where a.SeekerId == UserId && a.ListerId == job.UserId && a.JobId == JobId && a.IsApply.ToString() == "true" select a).FirstOrDefault();
                apps = (from n in DB.ApplicationStatus where n.SeekerId == UserId && n.ListerId == job.UserId && n.JobId == JobId && n.IsApply == true && n.IsAcceptedByLister.ToString() == "true" select n).FirstOrDefault();
                // 
                //apps = Applicant.Where(x => x.SeekerId == UserId && jobId==JobId && x.IsApply.ToString() == "true" && x.IsAcceptedByLister.ToString() == "true").FirstOrDefault();
                //&& x.IsApply.ToString() == "true" && x.IsAcceptedByLister.ToString() == "true"

                //foreach(var a in apps)
                //{

                //}
                jobdetail.JobId = JobId;
                jobdetail.ListerId = job.UserId;
                jobdetail.ListerName = listerDetail.FirstName + " " + listerDetail.LastName;
                jobdetail.ListerProfilePicture = path + listerDetail.ProfilePicture;
                jobdetail.ListerCompleteJob = listercompleteJob.Count();
                jobdetail.JobTitle = job.JobTitle;
                jobdetail.Latitude = job.Latitude;
                jobdetail.JobCategory = job.JobCategory;
                jobdetail.JobDescription = job.JobDescription;
                jobdetail.JobPicture = path + job.JobPicture;
                jobdetail.FromDate = job.FromDate;
                jobdetail.HowLong = job.HowLong;
                jobdetail.CompletionDate = job.CompletedDate;
                jobdetail.Amount = job.Amount == null ? 0 : job.Amount;
                jobdetail.Location = job.Location;
                jobdetail.Latitude = job.Latitude;
                jobdetail.Longitude = job.Longitude;
                jobdetail.AmountWithAdminCharges = job.AmountWithAdminCharges == null ? 0 : job.AmountWithAdminCharges;
                jobdetail.imageList = JobImage;
                jobdetail.ApplicantCount = Applicant.Count();
                var AzzidaVerify = (from x in DB.UserMasters where x.Id == jobdetail.ListerId select x.AzzidaVerified).FirstOrDefault();
                if (AzzidaVerify != null)
                {
                    jobdetail.AzzidaVarified = AzzidaVerify;
                }
                else
                {
                    jobdetail.AzzidaVarified = false;
                }
                if (gettippaymentid != null)
                {
                    jobdetail.TipId = gettippaymentid.Id;
                }
                else
                {
                    jobdetail.TipId = 0;
                }

                if (getpaymentId != null)
                {
                    jobdetail.PymntId = getpaymentId.Id;
                }
                else
                {
                    jobdetail.PymntId = 0;
                }
                if (job.IsCancel == true)
                {
                    jobdetail.Status = "Cancelled";
                }
                else if (Convert.ToDouble(job.FromDate) < milos)
                {
                    jobdetail.Status = "Expired";
                }
                else
                {
                    jobdetail.Status = "";
                }
                if (job.IsComplete == false)
                {
                    jobdetail.IsComplete = false;
                }
                if (job.IsComplete == null)
                {
                    jobdetail.IsComplete = false;
                }
                if (job.IsComplete == true)
                {
                    jobdetail.IsComplete = true;
                }

                jobdetail.IsJobComplete = job.IscompleteUser == null ? false : job.IscompleteUser;
                if (app != null)
                {
                    jobdetail.IsApply = true;
                }
                else
                {
                    jobdetail.IsApply = false;
                }

                if (apps != null)
                {
                    //jobdetail.IsApply = true;
                    jobdetail.ApplicationAccepted = true;
                    jobdetail.SeekerId = apps.SeekerId;
                    var Seekerdetails = (from um in DB.UserMasters where um.Id == apps.SeekerId select um).FirstOrDefault();
                    jobdetail.SeekerName = Seekerdetails.UserName;
                    jobdetail.Seekerimage = path + Seekerdetails.ProfilePicture;
                    if (apps.IsAccteptedBySeeker == true)
                    {
                        jobdetail.OfferAccepted = true;
                    }
                    else
                    {
                        jobdetail.OfferAccepted = false;
                    }
                }
                else
                {
                    //jobdetail.IsApply = false;
                    jobdetail.ApplicationAccepted = false;
                    jobdetail.OfferAccepted = false;
                }


            }

            return jobdetail;
        }


        public List<GetUserJob> GetMyListing(int UserId)
        {
            var data = (from a in DB.Jobs
                        where a.UserId == UserId && a.IscompleteUser.ToString() == "false"
                        && a.IsCancel.ToString() == "false"
                        select new GetUserJob
                        {
                            Id = a.Id,
                            JobTitle = a.JobTitle,
                            JobCategory = a.JobCategory,
                            JobDescription = a.JobDescription,
                            // JobPicture = path + a.JobPicture,
                            Location = a.Location,
                            Longitude = a.Longitude,
                            Latitude = a.Latitude,
                            UserId = a.UserId,
                            IsComplete = a.IsComplete,
                            HowLong = a.HowLong,
                            FromDate = a.FromDate,
                            CompletedDate = a.CompletedDate,
                            Amount = a.Amount

                        }).ToList();


            var applicationst = (from b in DB.ApplicationStatus where b.ListerId == UserId select b).ToList();
            GetUserJob myjob = new GetUserJob();
            List<GetUserJob> myList = new List<GetUserJob>();
            foreach (var a in data)
            {
                myjob = new GetUserJob();
                var appStatus = applicationst.Where(x => x.JobId == a.Id
                && x.IsAcceptedByLister.ToString() == "true").FirstOrDefault();
                myjob.Id = a.Id;
                myjob.UserId = a.UserId;
                myjob.JobTitle = a.JobTitle;
                myjob.JobCategory = a.JobCategory;
                myjob.JobDescription = a.JobDescription;
                //myjob.JobPicture = a.JobPicture;
                myjob.IsComplete = a.IsComplete == null ? false : a.IsComplete;
                myjob.CompletedDate = a.CompletedDate == null ? "" : a.CompletedDate;
                myjob.FromDate = a.FromDate;
                myjob.Location = a.Location;
                myjob.HowLong = a.HowLong == null ? "" : a.HowLong;
                myjob.Amount = a.Amount == null ? 0 : a.Amount;
                myjob.Latitude = a.Latitude;
                myjob.Longitude = a.Longitude;
                if (appStatus != null)
                {

                    if (appStatus.IsAcceptedByLister.ToString() == "true")
                    {
                        myjob.ApplicationAccept = true;
                    }
                    else
                    {
                        myjob.ApplicationAccept = false;
                    }
                    if (appStatus.IsAccteptedBySeeker.ToString() == "true")
                    {
                        myjob.OfferAccept = true;
                    }
                    else
                    {
                        myjob.OfferAccept = false;
                    }
                }
                else
                {
                    myjob.ApplicationAccept = false;
                    myjob.OfferAccept = false;
                }

                //jobimage

                var jobImge = (from s in DB.JobImages
                               where s.JobId == a.Id
                               select new JobImageList
                               {
                                   Id = s.Id,
                                   JobId = s.JobId,
                                   UserId = s.UserId,
                                   ImageName = path + s.ImageName

                               }).FirstOrDefault();
                if (jobImge != null)
                {
                    myjob.JobPicture = jobImge.ImageName;
                }


                myList.Add(myjob);
                myList = myList.OrderByDescending(x => x.Id).ToList();
            }

            return myList;
        }

        //Create account
        //public UserMaster CreateUserStripeAccount()
        //{
        //    var chargeService = new StripeChargeService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);
        //    StripeConfiguration.ApiKey = "sk_test_4eC39HqLyjWDarjtT1zdp7dc";

        //    var options = new AccountCreateOptions
        //    {
        //        Type = "custom",
        //        Country = "US",
        //        Email = "jenny.rosen@example.com",
        //        Capabilities = new AccountCapabilitiesOptions
        //        {
        //            CardPayments = new AccountCapabilitiesCardPaymentsOptions
        //            {
        //                Requested = true,
        //            },
        //            Transfers = new AccountCapabilitiesTransfersOptions
        //            {
        //                Requested = true,
        //            },
        //        },
        //    };
        //    var service = new AccountService();
        //    service.Create(options);
        //}

        //payment history admin 
        public List<PaymentHistoryData> GetPaymentHistoryList()
        {
            var data = (from j in DB.Jobs
                        join t in DB.Payments on j.Id equals t.JobId
                        join um in DB.UserMasters on j.UserId equals um.Id
                        join u in DB.UserMasters on j.AssignSeekerId equals u.Id
                        select new PaymentHistoryData
                        {
                            JobId = j.Id,
                            JobAmount = j.Amount,
                            ListerId = j.UserId,
                            ListerName = um.UserName,
                            SeekerId = j.AssignSeekerId,
                            SeekerName = u.UserName,
                            TotalAmount = t.TotalAmount,
                            paymentType = t.PaymentType,
                            JobTitle = j.JobTitle,
                            IsComplete = j.IsComplete,
                            IscompleteUser = j.IscompleteUser,
                            PaymentTime = Convert.ToDateTime(t.CreatedDate),
                            IsSeekerPaymentDone = t.IsSeekerPaymentDone == null ? false : t.IsSeekerPaymentDone,
                            IsListerPaymentDone = t.IsSuccess == null ? false : t.IsSuccess
                        }).ToList();
            List<PaymentHistoryData> ph = new List<PaymentHistoryData>();
            PaymentHistoryData p = new PaymentHistoryData();
            foreach (var a in data)
            {
                p = new PaymentHistoryData();
                p.JobId = a.JobId;
                p.JobAmount = a.JobAmount == null ? 0 : a.JobAmount;
                p.ListerId = a.ListerId;
                p.ListerName = a.ListerName;
                p.SeekerId = a.SeekerId;
                p.SeekerName = a.SeekerName;

                p.TotalAmount = a.TotalAmount == null ? 0 : a.TotalAmount;
                p.paymentType = a.paymentType;
                p.JobTitle = a.JobTitle;
                p.IsComplete = a.IsComplete;
                p.IscompleteUser = a.IscompleteUser;
                if (a.IsListerPaymentDone == true && a.IsSeekerPaymentDone == false)
                {
                    p.Status = "Pending";
                }
                if (a.IsListerPaymentDone == true && a.IsSeekerPaymentDone == true)
                {
                    p.Status = "Success";
                }
                p.PaymentTime = a.PaymentTime;
                ph.Add(p);
            }

            return ph;
        }


        //app payment transaction 
        public List<ViewSender> GetViewTransaction(int UserId)
        {
            var Sender = (from a in DB.Tips
                          join um in DB.UserMasters
                          on a.SeekerId equals um.Id
                          join j in DB.Jobs on a.JobId equals j.Id
                          where a.UserId == UserId
                          select new ViewSender
                          {
                              Id = a.Id,
                              ListerId = a.UserId,
                              PaidTo = a.SeekerId,
                              JobId = a.JobId,
                              JobTitle = j.JobTitle,

                              TotalAmount = a.TotalAmount == null ? 0 : a.TotalAmount,
                              CreatedDate = a.CreatedDate,
                              ToName = um.FirstName + " " + um.LastName,
                              ToProfilePicture = path + um.ProfilePicture,

                          }).ToList();

            var Receiver = (from b in DB.Tips
                            join um in DB.UserMasters on b.UserId equals um.Id
                            join j in DB.Jobs on b.JobId equals j.Id
                            where b.SeekerId == UserId
                            select new ViewSender
                            {
                                Id = b.Id,
                                ReceivedFrom = b.UserId,
                                MyId = b.SeekerId,
                                JobId = b.JobId,
                                JobTitle = j.JobTitle,
                                TotalAmount = b.TotalAmount == null ? 0 : b.TotalAmount,
                                CreatedDate = b.CreatedDate,
                                SenderName = um.FirstName + " " + um.LastName,
                                SenderProfilePicture = path + um.ProfilePicture,
                            }).ToList();

            ViewSender vpSendr = new ViewSender();
            List<ViewSender> vpsndrlist = new List<ViewSender>();

            ViewSender vpreceiver = new ViewSender();
            List<ViewSender> vpreceiverlist = new List<ViewSender>();
            List<ViewPayment> viewPay = new List<ViewPayment>();
            foreach (var i in Sender)
            {
                vpSendr = new ViewSender();
                vpSendr.Id = i.Id;
                vpSendr.ListerId = i.ListerId;
                vpSendr.PaidTo = i.PaidTo;
                vpSendr.ToName = i.ToName;
                vpSendr.ToProfilePicture = i.ToProfilePicture;
                vpSendr.JobId = i.JobId;
                vpSendr.JobTitle = i.JobTitle;
                vpSendr.TotalAmount = i.TotalAmount == null ? 0 : i.TotalAmount;
                vpSendr.CreatedDate = i.CreatedDate;

                vpsndrlist.Add(vpSendr);
            }

            foreach (var j in Receiver)
            {
                vpreceiver = new ViewSender();
                vpreceiver.Id = j.Id;
                vpreceiver.ReceivedFrom = j.ReceivedFrom;
                vpreceiver.MyId = j.MyId;
                vpreceiver.JobId = j.JobId;
                vpreceiver.JobTitle = j.JobTitle;
                vpreceiver.SenderName = j.SenderName;
                vpreceiver.SenderProfilePicture = j.SenderProfilePicture;
                vpreceiver.TotalAmount = j.TotalAmount == null ? 0 : j.TotalAmount;
                vpreceiver.CreatedDate = j.CreatedDate;

                vpreceiverlist.Add(vpreceiver);
            }
            vpsndrlist.AddRange(vpreceiverlist);
            vpsndrlist = vpsndrlist.OrderByDescending(x => x.CreatedDate).ToList();
            //viewPay.SenderList = vpsndrlist;
            //viewPay.ReceiverList = vpreceiverlist;
            return vpsndrlist;
        }


        public List<ViewSender> PaymentHistory(int UserId)
        {
            var userData = (from s in DB.UserMasters select s).ToList();
            var data = (from j in DB.Jobs select j).ToList();
            var Sender = (from a in DB.Payments
                              //   join um in DB.UserMasters
                              //  on a.ToUserId equals um.Id
                              //join j in DB.Jobs on a.JobId equals j.Id
                          where a.UserId == UserId
                          select new ViewSender
                          {
                              Id = a.Id,
                              ListerId = a.UserId,
                              PaidTo = a.ToUserId,
                              JobId = a.JobId,

                              PaymentType = a.PaymentType,
                              TotalAmount = a.TotalAmount == null ? 0 : a.TotalAmount,
                              IsListerPaymentDone = a.IsSuccess == null ? false : Convert.ToBoolean(a.IsSuccess),
                              IsSeekerPaymentDone = a.IsSeekerPaymentDone == null ? false : Convert.ToBoolean(a.IsSeekerPaymentDone),
                              CreatedDate = Convert.ToDateTime(a.CreatedDate),
                              ToName = "",
                              ToProfilePicture = "",

                          }).ToList();

            var Receiver = (from b in DB.Payments
                                //  join um in DB.UserMasters on b.UserId equals um.Id
                            join j in DB.Jobs on b.JobId equals j.Id
                            where b.ToUserId == UserId && (b.PaymentType.ToLower() == "payment" || b.PaymentType.ToLower() == "tip")
                            select new ViewSender
                            {
                                Id = b.Id,
                                ReceivedFrom = b.UserId,
                                MyId = b.ToUserId,
                                JobId = b.JobId,
                                JobTitle = j.JobTitle,
                                JobAmount = j.Amount,
                                //FeesPaid = j.AmountWithAdminCharges == null ? 0 : j.AmountWithAdminCharges-j.Amount,
                                PaymentType = b.PaymentType,
                                TotalAmount = b.TotalAmount == null ? 0 : b.TotalAmount,
                                IsListerPaymentDone = b.IsSuccess == null ? false : Convert.ToBoolean(b.IsSuccess),
                                IsSeekerPaymentDone = b.IsSeekerPaymentDone == null ? false : Convert.ToBoolean(b.IsSeekerPaymentDone),
                                SeekerPaymentAmount = b.SeekerPaymentAmount,
                                CreatedDate = Convert.ToDateTime(b.CreatedDate),
                                SenderName = "",
                                SenderProfilePicture = "",
                            }).ToList();

            ViewSender vpSendr = new ViewSender();
            List<ViewSender> vpsndrlist = new List<ViewSender>();

            ViewSender vpreceiver = new ViewSender();
            List<ViewSender> vpreceiverlist = new List<ViewSender>();
            List<ViewPayment> viewPay = new List<ViewPayment>();
            UserMaster user = new UserMaster();
            foreach (var i in Sender)
            {

                vpSendr = new ViewSender();
                user = userData.Where(x => x.Id == i.PaidTo).FirstOrDefault();
                var job = data.Where(x => x.Id == i.JobId).FirstOrDefault();
                vpSendr.Id = i.Id;
                vpSendr.ListerId = i.ListerId;
                vpSendr.PaidTo = i.PaidTo;

                vpSendr.PaymentType = i.PaymentType;
                if (user != null)
                {
                    if (i.PaymentType.ToLower() == "dispute" || i.PaymentType.ToLower() == "checker")
                    {
                        vpSendr.ToName = " ";
                        vpSendr.ToProfilePicture = "";
                    }
                    else
                    {
                        vpSendr.ToName = user.FirstName + " " + user.LastName;
                        vpSendr.ToProfilePicture = path + user.ProfilePicture;
                    }
                }
                else
                {
                    vpSendr.ToName = " ";
                    vpSendr.ToProfilePicture = " ";
                }

                if (i.IsListerPaymentDone == true)
                {
                    vpSendr.IsListerPaymentDone = true;
                }
                else
                {
                    vpSendr.IsListerPaymentDone = false;
                }
                if (i.IsSeekerPaymentDone == true)
                {
                    vpSendr.IsSeekerPaymentDone = true;
                }
                else
                {
                    vpSendr.IsSeekerPaymentDone = false;
                }
                if (job == null || i.PaymentType.ToLower() == "checker" || i.PaymentType.ToLower() == "tip" || i.PaymentType.ToLower() == "dispute")
                {
                    vpSendr.JobAmount = 0;

                }
                else
                {
                    vpSendr.JobAmount = job.Amount;

                }
                if (job == null || i.PaymentType.ToLower() == "checker")
                {
                    vpSendr.JobId = 0;
                    vpSendr.JobTitle = "";

                }
                else
                {
                    vpSendr.JobId = job.Id;
                    vpSendr.JobTitle = job.JobTitle;

                }
                vpSendr.TotalAmount = i.TotalAmount == null ? 0 : i.TotalAmount;
                vpSendr.CreatedDate = i.CreatedDate;

                vpsndrlist.Add(vpSendr);
            }

            foreach (var j in Receiver)
            {
                //if (!string.IsNullOrEmpty(Convert.ToString(j.SeekerPaymentAmount))) { }
                vpreceiver = new ViewSender();
                user = userData.Where(x => x.Id == j.ReceivedFrom).FirstOrDefault();
                vpreceiver.Id = j.Id;
                vpreceiver.ReceivedFrom = j.ReceivedFrom;
                vpreceiver.MyId = j.MyId;
                vpreceiver.JobId = j.JobId;
                vpreceiver.PaymentType = j.PaymentType;
                if (vpreceiver.PaymentType.ToLower() == "dispute" || vpreceiver.PaymentType.ToLower() == "checker")
                {
                    vpreceiver.ToName = user.FirstName + " " + user.LastName;
                    vpreceiver.ToProfilePicture = path + user.ProfilePicture;
                }
                else
                {
                    vpreceiver.SenderName = user.FirstName + " " + user.LastName;
                    vpreceiver.SenderProfilePicture = path + user.ProfilePicture;
                }

                vpreceiver.JobTitle = j.JobTitle;
                if (j.PaymentType.ToLower() == "checker" || j.PaymentType.ToLower() == "tip" || j.PaymentType.ToLower() == "dispute")
                {
                    vpreceiver.JobAmount = 0;

                }
                else
                {
                    vpreceiver.JobAmount = j.JobAmount;

                }
                // vpreceiver.JobAmount = j.JobAmount;
                // vpreceiver.FeesPaid = j.FeesPaid;
                //vpreceiver.SenderName = user.FirstName + " " + user.LastName;
                if (j.IsListerPaymentDone == true)
                {
                    vpreceiver.IsListerPaymentDone = true;
                }
                else
                {
                    vpreceiver.IsListerPaymentDone = false;
                }
                if (j.IsSeekerPaymentDone == true)
                {
                    vpreceiver.IsSeekerPaymentDone = true;
                }
                else
                {
                    vpreceiver.IsSeekerPaymentDone = false;
                }
                //vpreceiver.SenderProfilePicture = j.SenderProfilePicture;
                vpreceiver.TotalAmount = j.TotalAmount == null ? 0 : j.TotalAmount;
                vpreceiver.CreatedDate = j.CreatedDate;
                vpreceiver.SeekerPaymentAmount = j.SeekerPaymentAmount;

                vpreceiverlist.Add(vpreceiver);
            }
            vpsndrlist.AddRange(vpreceiverlist);
            vpsndrlist = vpsndrlist.OrderByDescending(x => x.CreatedDate).ToList();
            //viewPay.SenderList = vpsndrlist;
            //viewPay.ReceiverList = vpreceiverlist;
            return vpsndrlist;
        }


        public GetUserJob GetJobById(int JobId)
        {
            var jobImge = (from s in DB.JobImages
                           where s.JobId == JobId
                           select new JobImageList
                           {
                               Id = s.Id,
                               JobId = s.JobId,
                               UserId = s.UserId,
                               ImageName = path + s.ImageName

                           }).ToList();
            //myjob.imglist = jobImge;

            var data = (from a in DB.Jobs
                        where a.Id == JobId
                        select new GetUserJob
                        {
                            Id = a.Id,
                            UserId = a.UserId,
                            JobTitle = a.JobTitle,
                            JobCategory = a.JobCategory,
                            JobDescription = a.JobDescription,
                            JobPicture = path + a.JobPicture,
                            Location = a.Location,
                            Latitude = a.Latitude,
                            Longitude = a.Longitude,
                            HowLong = a.HowLong,
                            FromDate = a.FromDate,
                            Amount = a.Amount == null ? 0 : a.Amount,
                            imglist = jobImge
                        }).FirstOrDefault();
            return data;
        }

        public DisputeResolution GetDisputeById(int Id)
        {
            var data = (from a in DB.DisputeResolutions where a.Id == Id select a).FirstOrDefault();
            return data;
        }

        public List<GetUserJob> GetJobList()
        {
            var data = (from a in DB.Jobs
                        select new GetUserJob
                        {
                            Id = a.Id,
                            UserId = a.UserId,
                            JobTitle = a.JobTitle,
                            JobCategory = a.JobCategory,
                            JobDescription = a.JobDescription,
                            JobPicture = a.JobPicture,
                            IsComplete = a.IsComplete,
                            CompletedDate = a.CompletedDate,
                            FromDate = a.FromDate,
                            Location = a.Location,
                            HowLong = a.HowLong,
                            Amount = a.Amount == null ? 0 : a.Amount,
                            Latitude = a.Latitude,
                            Longitude = a.Longitude,
                        }).ToList();



            return data;
        }

        public List<postAssociate> GetPostAssociate(int UserId)
        {
            double milos = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            List<Job> Createjob = new List<Job>();
            List<Job> applyjob = new List<Job>();
            List<Job> job = new List<Job>();
            var application = (from b in DB.ApplicationStatus where b.SeekerId == UserId && b.NotSelected.ToString() == "false" select b).ToList();
            Createjob = (from j in DB.Jobs
                         where j.UserId == UserId && j.IsComplete.ToString() == "false"
                         && j.IsCancel.ToString() == "false" && Convert.ToDouble(j.FromDate) > milos
                         select j).ToList();
            foreach (var s in application)
            {
                var data1 = (from aj in DB.Jobs
                             where aj.Id == s.JobId && aj.IsComplete.ToString() == "false"
      && aj.IsCancel.ToString() == "false" && Convert.ToDouble(aj.FromDate) > milos
                             select aj).FirstOrDefault();
                applyjob.Add(data1);
            }
            job.AddRange(applyjob);
            job.AddRange(Createjob);
            List<postAssociate> data = new List<postAssociate>();

            foreach (var a in job)
            {
                if (a != null)
                {
                    postAssociate pa = new postAssociate();
                    pa.postassociate = "Job #" + " " + a.JobTitle;
                    pa.Amount = a.Amount == null ? 0 : a.Amount;
                    pa.Id = a.Id;
                    data.Add(pa);
                }
                //var data = (from a in job
                //       // where
                //        //a.UserId != UserId
                //       // a.IsComplete.ToString() == "false"
                //       //&& a.IsCancel.ToString() == "false" && Convert.ToDouble(a.FromDate) > milos
                //        select new postAssociate
                //        {
                //            postassociate = "ref #" + a.Id + " " + a.JobTitle
                //        }).ToList();


            }
            return data;
        }
        //update user Lat long
        public UserMaster UpdateUserLatLong(int Id, string Latitude, string Longitude)
        {
            var data = (from a in DB.UserMasters where a.Id == Id select a).FirstOrDefault();
            data.UserLatitude = Latitude;
            data.UserLongitude = Longitude;
            DB.SubmitChanges();
            return data;
        }

        //logout
        public void Logout(int UserId)
        {
            var data = (from a in DB.UserMasters where a.Id == UserId select a).FirstOrDefault();
            if (data != null)
            {
                data.DeviceId = "";
                data.DeviceType = "";
                DB.SubmitChanges();
            }

        }

        //GetBounsBalance

        public ReferalBalance GetReferalBalance(int UserId)
        {
            var data = (from rb in DB.ReferalBalances where rb.UserId == UserId select rb).FirstOrDefault();
            return data;
        }

        //deletecard

        public void DeleteCardById(int cardId)
        {
            var data = (from a in DB.UserCards where a.Id == cardId select a).FirstOrDefault();
            if (data != null)
            {
                DB.UserCards.DeleteOnSubmit(data);
                DB.SubmitChanges();
            }
        }

        //save card
        public SaveCardResponse SaveCard(string tokenid, int userid)
        {
            string err = "";
            try
            {
                if (tokenid != "")
                {
                    var customerOptions = new StripeCustomerCreateOptions
                    {
                        SourceToken = tokenid,
                        Email = tokenid + "@gmail.com",
                    };
                    var customerService = new StripeCustomerService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);
                    StripeCustomer customer = customerService.Create(customerOptions);
                    if (customer != null)
                    {
                        var card = (from t in DB.UserCards where t.UserId == userid && t.CardNumber == customer.Sources.Data[0].Card.Last4 && t.CardType == customer.Sources.Data[0].Card.Brand select t).FirstOrDefault();
                        if (card == null)
                        {
                            UserCard uc = new UserCard();
                            uc.CustomerId = customer.Id;
                            uc.UserId = userid;
                            uc.CardNumber = customer.Sources.Data[0].Card.Last4;
                            uc.ExpiryMonth = customer.Sources.Data[0].Card.ExpirationMonth.ToString();
                            uc.ExpiryYear = customer.Sources.Data[0].Card.ExpirationYear.ToString();
                            uc.CardType = customer.Sources.Data[0].Card.Brand;


                            DB.UserCards.InsertOnSubmit(uc);
                            DB.SubmitChanges();
                        }
                    }

                }
            }
            catch (Exception ex) { err = ex.Message; }
            SaveCardResponse s = new SaveCardResponse();
            s.error = err;
            s.usercards = (from t in DB.UserCards where t.UserId == userid select t).ToList();
            return s;
        }
        //getcustomer card

        public List<UserCard> GetCustomerCards(int userid)
        {
            return (from t in DB.UserCards where t.UserId == userid select t).ToList();
        }

        public void ToSeekerPayment(int paymentId, int JobId, int UserId, int ToUserId, decimal TotalAmount)
        {
            try
            {
                var ToUserAccountId = (from a in DB.UserMasters where a.Id == ToUserId select a).FirstOrDefault();
                // var jobAmount = (from j in DB.Jobs where j.Id == JobId select j).FirstOrDefault();
                var fee = (from f in DB.JobFees select f).FirstOrDefault();
                var paymentData = (from py in DB.Payments where py.Id == paymentId && py.PaymentType.ToLower() == "payment" select py).FirstOrDefault();
                var accountService = new StripeAccountService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);
                //"sk_test_QS7zwe52WufPIZHXjwnpgj1D");
                // StripeAccount account = accountService.Get("acct_19unI3J1Y72ANlMu");
                //acct_1CHlgSIoFMBSbD2s


                var ddd = new StripeTransferService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);
                //  bidamount = bidamount - (bidamount * Convert.ToDecimal(0.10));
                // decimal paymentAmount = (jobAmount.Amount) - (jobAmount.Amount) * (fee.JobSeekerFee / 100) ?? 0;
                decimal paymentAmount = (TotalAmount) - (TotalAmount) * (fee.JobSeekerFee / 100) ?? 0;


                if (!string.IsNullOrEmpty(ToUserAccountId.StripeAccId))
                {
                    var myCharge12 = new StripeTransferCreateOptions
                    {
                        Amount = Convert.ToInt32(paymentAmount * Convert.ToDecimal(100)),
                        Currency = "usd",
                        //SourceTransaction =d.FirstOrDefault().TokenId,  // "txn_1CNImMIoFMBSbD2sPhE8O8co",
                        Destination = ToUserAccountId.StripeAccId,//"acct_179XbqHpdOej4gWA",
                                                                  // TransferGroup = jobid.ToString(),
                                                                  // SourceType = "card"
                        TransferGroup = JobId.ToString()

                    };
                    StripeTransfer stripeCharge1 = ddd.Create(myCharge12);

                    if (paymentData != null)
                    {
                        paymentData.SeekerPaymentAmount = paymentAmount;
                        paymentData.ModifyDate = System.DateTime.Now.ToString();
                        DB.SubmitChanges();

                    }
                }
                //string str = "Success";
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            //   return paymentData;
        }

        //make payment
        public PaymentDetails CreatePayment(int JobId, int UserId, int ToUserId, decimal refbalance, string CustomerId, decimal TotalAmount, string PaymentToken, string PaymentType)
        {
            //decimal Tax, decimal Amount,
            //var chargeService = new StripeChargeService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);


            //paymentprocess

            Payment p = new Payment();
            //decimal paymentAmount = 0;
            if (PaymentType.ToUpper() == "DISPUTE")
            {

                p = PaymentProcess(JobId, UserId, ToUserId, refbalance, CustomerId, TotalAmount, PaymentToken, PaymentType);
                //var chargeService = new StripeChargeService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);
                //////if (refbalance > TotalAmount)
                //////{
                //////    paymentAmount = TotalAmount;
                //////}
                //////if (refbalance < TotalAmount)
                //////{
                //////    paymentAmount = TotalAmount - refbalance;
                //////}
                //////if (refbalance == TotalAmount)
                //////{
                //////    paymentAmount = TotalAmount;
                //////}


                ////////  var paymentId = new StripeChargeService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);
                //////if (CustomerId != null)
                //////{
                //////    //if (PaymentToken != null)
                //////    //{
                //////        var myCharge2 = new StripeChargeCreateOptions
                //////        {
                //////            Amount = Convert.ToInt32(paymentAmount) * 100,
                //////            Currency = "usd",
                //////            Description = "Job payment",
                //////            CustomerId = CustomerId,
                //////            //SourceTokenOrExistingSourceId = token,
                //////            Capture = true,
                //////            TransferGroup = JobId.ToString()

                //////        };
                //////        paymentId = chargeService.Create(myCharge2);
                //////    //}
                //////}
                //////else
                //////{


                //////    if (PaymentToken != null)
                //////    {
                //////        var myCharge = new StripeChargeCreateOptions
                //////        {
                //////            Amount = Convert.ToInt32(paymentAmount * 100),
                //////            Currency = "usd",
                //////            Description = "Job Payment",
                //////            //CustomerId = custid,
                //////            SourceTokenOrExistingSourceId = PaymentToken,
                //////            Capture = true
                //////        };
                //////        paymentId = chargeService.Create(myCharge);
                //////    }
                //////}



                //////p.JobId = JobId;
                //////p.UserId = UserId;
                //////p.ToUserId = ToUserId;

                //////p.Amount = paymentAmount;
                //////p.TotalAmount = TotalAmount;
                //////p.CustomerId = CustomerId;

                //////if (PaymentToken != null)
                //////{
                //////    p.PaymentToken = PaymentToken;
                //////    p.PaymentId = paymentId.Id;

                //////    if (paymentId.Paid == true)
                //////    {
                //////        p.IsSuccess = true;
                //////    }
                //////    else
                //////    {
                //////        p.IsSuccess = false;
                //////    }
                //////}
                //////else
                //////{
                //////    p.PaymentToken = "";
                //////    p.PaymentId = paymentId.Id;
                //////    p.IsSuccess = true;
                //////}
                //////p.PaymentType = PaymentType;
                //////p.CreatedDate = Convert.ToString(DateTime.Now);
                //////DB.Payments.InsertOnSubmit(p);
                //////DB.SubmitChanges();


                //  return p;

            }
            else
            {
                p = PaymentProcess(JobId, UserId, ToUserId, refbalance, CustomerId, TotalAmount, PaymentToken, PaymentType);
                ////if (refbalance > TotalAmount)
                ////{
                ////    paymentAmount = TotalAmount;
                ////}
                ////if (refbalance < TotalAmount)
                ////{
                ////    paymentAmount = TotalAmount - refbalance;
                ////}
                ////if (refbalance == TotalAmount)
                ////{
                ////    paymentAmount = TotalAmount;
                ////}

                //////  var paymentId = new StripeChargeService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);
                ////if (CustomerId != null)
                ////{

                ////        var myCharge2 = new StripeChargeCreateOptions
                ////        {
                ////            Amount = Convert.ToInt32(paymentAmount) * 100,
                ////            Currency = "usd",
                ////            Description = "Job Payment",
                ////            CustomerId = CustomerId,
                ////            //SourceTokenOrExistingSourceId = token,
                ////            Capture = true,
                ////            TransferGroup = JobId.ToString()

                ////        };
                ////        paymentId = chargeService.Create(myCharge2);

                ////}
                ////else
                ////{

                ////    if (PaymentToken != null)
                ////    {
                ////        var myCharge = new StripeChargeCreateOptions
                ////        {
                ////            Amount = Convert.ToInt32(paymentAmount * 100),
                ////            Currency = "usd",
                ////            Description = "Job Payment",
                ////            //CustomerId = custid,
                ////            SourceTokenOrExistingSourceId = PaymentToken,
                ////            Capture = true
                ////        };
                ////        paymentId = chargeService.Create(myCharge);
                ////    }
                ////}


                //////Payment p = new Payment();
                ////p.JobId = JobId;
                ////p.UserId = UserId;
                ////p.ToUserId = ToUserId;

                ////p.Amount = paymentAmount;
                ////p.TotalAmount = TotalAmount;
                ////p.CustomerId = CustomerId;

                ////if (PaymentToken != null)
                ////{
                ////    p.PaymentToken = PaymentToken;
                ////    p.PaymentId = paymentId.Id;

                ////    if (paymentId.Paid == true)
                ////    {
                ////        p.IsSuccess = true;
                ////    }
                ////    else
                ////    {
                ////        p.IsSuccess = false;
                ////    }
                ////}
                ////else
                ////{
                ////    p.PaymentToken = "";
                ////    p.PaymentId = paymentId.Id;
                ////    p.IsSuccess = true;
                ////}
                ////p.PaymentType = PaymentType;
                ////p.CreatedDate = Convert.ToString(DateTime.Now);
                ////DB.Payments.InsertOnSubmit(p);
                ////DB.SubmitChanges();



            }


            //update from user ref balance
            if (refbalance > 0)
            {
                var fromUserRef = (from f in DB.ReferalBalances where f.UserId == UserId select f).FirstOrDefault();
                if (fromUserRef != null)
                {
                    decimal reaminingAmount;

                    if (fromUserRef.Amount >= refbalance)
                    {
                        reaminingAmount = Convert.ToDecimal(fromUserRef.Amount) - Convert.ToDecimal(refbalance);
                        fromUserRef.Amount = reaminingAmount;
                    }
                    //if (fromUserRef.Amount < refbalance)
                    //{
                    //    reaminingAmount = 0;
                    //    fromUserRef.Amount = reaminingAmount;
                    //}
                    //if (fromUserRef.Amount == refbalance)
                    //{
                    //    fromUserRef.Amount = 0;
                    //}
                    fromUserRef.ModifyDate = System.DateTime.Now;
                    DB.SubmitChanges();
                }
            }



            //update To user ref balance
            //if (p.IsSuccess == true && ToUserId != 0)
            //{
            //    var ToUserRef = (from b in DB.ReferalBalances where b.UserId == ToUserId select b).FirstOrDefault();
            //    if (ToUserRef != null)
            //    {
            //        ToUserRef.Amount = ToUserRef.Amount + TotalAmount;
            //        ToUserRef.ModifyDate = System.DateTime.Now;
            //        DB.SubmitChanges();
            //    }
            //    else
            //    {
            //        ReferalBalance rb = new ReferalBalance();
            //        rb.UserId = ToUserId;
            //        rb.Amount = TotalAmount;
            //        rb.CreateDate = System.DateTime.Now;
            //        DB.ReferalBalances.InsertOnSubmit(rb);
            //        DB.SubmitChanges();
            //    }



            //}
            var data = (from b in DB.ReferalBalances where b.UserId == UserId select b).FirstOrDefault();
            decimal rfb = 0;
            if (data != null)
            {
                rfb = data.Amount ?? 0;
            }
            var resp = (from a in DB.Payments
                            // join rf in DB.ReferalBalances on a.UserId equals rf.UserId
                        where a.Id == p.Id
                        select new PaymentDetails
                        {
                            Id = a.Id,
                            JobId = a.JobId,
                            UserId = a.UserId,
                            RefBalance = rfb,
                            ToUserId = a.ToUserId,
                            Amount = a.Amount == null ? 0 : a.Amount,
                            TotalAmount = a.TotalAmount == null ? 0 : a.TotalAmount,
                            CreatedDate = a.CreatedDate,
                            PaymentToken = a.PaymentToken,
                            PaymentType = a.PaymentType,
                            paymentId = a.PaymentId,
                            IsSuccess = a.IsSuccess
                        }).FirstOrDefault();
            return resp;
        }

        //payment method
        public Payment PaymentProcess(int JobId, int UserId, int ToUserId, decimal refbalance, string CustomerId, decimal TotalAmount, string PaymentToken, string PaymentType)
        {
            Payment p = new Payment();
            decimal paymentAmount = 0;
            var chargeService = new StripeChargeService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);
            //if (refbalance > TotalAmount)
            //{
            //    paymentAmount = TotalAmount;
            //}
            if (refbalance < TotalAmount)
            {
                paymentAmount = TotalAmount - refbalance;
            }
            if (refbalance == TotalAmount)
            {
                paymentAmount = TotalAmount;
            }

            //  var paymentId = new StripeChargeService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);
            if (CustomerId != null)
            {
                //if (PaymentToken != null)
                //{
                var myCharge2 = new StripeChargeCreateOptions
                {
                    Amount = Convert.ToInt32(paymentAmount) * 100,
                    Currency = "usd",
                    Description = "Job payment",
                    CustomerId = CustomerId,
                    //SourceTokenOrExistingSourceId = PaymentToken,
                    Capture = true,
                    TransferGroup = JobId.ToString()

                };
                paymentId = chargeService.Create(myCharge2);
                //}
            }
            else
            {


                if (PaymentToken != null)
                {
                    var myCharge = new StripeChargeCreateOptions
                    {
                        Amount = Convert.ToInt32(paymentAmount * 100),
                        Currency = "usd",
                        Description = "Job Payment",
                        //CustomerId = custid,
                        SourceTokenOrExistingSourceId = PaymentToken,
                        TransferGroup = JobId.ToString(),
                        Capture = true
                    };
                    paymentId = chargeService.Create(myCharge);
                }
            }



            p.JobId = JobId;
            p.UserId = UserId;
            p.ToUserId = ToUserId;

            p.Amount = paymentAmount;
            p.TotalAmount = TotalAmount;
            p.CustomerId = CustomerId;
            if (CustomerId != null)
            {
                p.PaymentToken = "";
                p.PaymentId = paymentId.Id;
                if (paymentId.Paid == true)
                {
                    p.IsSuccess = true;
                }
                else
                {
                    p.IsSuccess = false;
                }
            }
            if (PaymentToken != null)
            {
                p.PaymentToken = PaymentToken;
                p.PaymentId = paymentId.Id;

                if (paymentId.Paid == true)
                {
                    p.IsSuccess = true;
                }
                else
                {
                    p.IsSuccess = false;
                }
            }
            else
            {
                p.PaymentToken = "";
                p.PaymentId = "";
                p.IsSuccess = true;
            }

            if (PaymentType.ToLower() == "dispute" || PaymentType.ToLower() == "checker")
            {
                p.IsSuccess = true;
                p.IsSeekerPaymentDone = true;
            }
            p.PaymentType = PaymentType;
            p.CreatedDate = Convert.ToString(System.DateTime.Now);
            DB.Payments.InsertOnSubmit(p);
            DB.SubmitChanges();


            PaymentHistory ph = new PaymentHistory();
            ph.JobId = p.JobId ?? 0;
            ph.paymentId = p.Id;
            ph.UserId = p.UserId;
            ph.SeekerId = p.ToUserId;
            ph.ListerPaymentAmount = TotalAmount;
            if (p.PaymentType.ToLower() == "payment")
            {
                ph.TippingAmount = 0;
                ph.DisputeAmount = 0;
            }
            else if (p.PaymentType.ToLower() == "tip")
            {
                ph.TippingAmount = TotalAmount;
                ph.DisputeAmount = 0;
            }
            else
            {
                ph.TippingAmount = 0;
                ph.DisputeAmount = TotalAmount;
            }
            if (p.IsSuccess == true)
            {
                ph.IsListerPaymentDone = true;
                ph.PaymentStatus = "success";
            }
            else
            {
                ph.IsListerPaymentDone = false;
                ph.PaymentStatus = "faild";
            }


            DB.PaymentHistories.InsertOnSubmit(ph);
            DB.SubmitChanges();

            if (PaymentType.ToLower() == "checker" && p.IsSuccess == true)
            {
                var userAzzidaverify = (from u in DB.UserMasters where u.Id == UserId select u).FirstOrDefault();
                if (userAzzidaverify != null)
                {
                    userAzzidaverify.AzzidaVerified = true;
                    DB.SubmitChanges();
                }
            }
            return p;
        }


        public List<GetMyJob> GetJob(int UserId)
        {
            var data = (from a in DB.Jobs
                            //join b in DB.ApplicationStatus on a.Id equals b.JobId
                        where a.UserId != UserId && a.IsComplete.ToString() == "false"
                         && a.IsCancel.ToString() == "false"
                        select new GetMyJob
                        {
                            Id = a.Id,
                            JobTitle = a.JobTitle,
                            JobCategory = a.JobCategory,
                            JobDescription = a.JobDescription,

                            Location = a.Location,
                            Longitude = a.Longitude,
                            Latitude = a.Latitude,
                            UserId = a.UserId,
                            HowLong = a.HowLong,
                            IsComplete = a.IsComplete,
                            //ApplicationAccepted=b.IsAcceptedByLister!=true?false:true,
                            //offerAccecpted=b.IsAccteptedBySeeker!= true?false:true,
                            FromDate = a.FromDate,
                            CompletedDate = a.CompletedDate,
                            Amount = a.Amount == null ? 0 : a.Amount
                        }).ToList();

            var appicationst = (from b in DB.ApplicationStatus where b.SeekerId == UserId select b).ToList();
            GetMyJob job = new GetMyJob();
            List<GetMyJob> jobList = new List<GetMyJob>();
            foreach (var a in data)
            {
                job = new GetMyJob();
                var appicationstatus = appicationst.Where(x => x.JobId == a.Id).FirstOrDefault();
                job.Id = a.Id;
                job.JobTitle = a.JobTitle;
                job.JobCategory = a.JobCategory;
                job.JobDescription = a.JobDescription;
                //  job.JobPicture = path + a.JobPicture;
                job.Location = a.Location;
                job.Longitude = a.Longitude;
                job.Latitude = a.Latitude;
                job.UserId = a.UserId;
                job.IsComplete = a.IsComplete == null ? false : a.IsComplete;
                if (appicationstatus != null)
                {
                    if (appicationstatus.IsApply == true)
                    {
                        job.IsApplied = true;
                    }
                    if (appicationstatus.IsAcceptedByLister.ToString() == "true")
                    {
                        job.ApplicationAccepted = true;
                    }
                    else
                    {
                        job.ApplicationAccepted = false;
                    }
                    if (appicationstatus.IsAccteptedBySeeker.ToString() == "true")
                    {
                        job.offerAccecpted = true;
                    }
                    else
                    {
                        job.offerAccecpted = false;
                    }
                }
                else
                {
                    job.IsApplied = false;
                }
                job.FromDate = a.FromDate;
                job.CompletedDate = a.CompletedDate == null ? "" : a.CompletedDate;
                job.Amount = a.Amount;
                job.HowLong = a.HowLong == null ? "" : a.HowLong;
                var jobImge = (from s in DB.JobImages
                               where s.JobId == a.Id
                               select new JobImageList
                               {
                                   Id = s.Id,
                                   JobId = s.JobId,
                                   UserId = s.UserId,
                                   ImageName = path + s.ImageName

                               }).FirstOrDefault();
                if (jobImge != null)
                {
                    job.JobPicture = jobImge.ImageName;
                }



                jobList.Add(job);
            }

            return jobList;
        }



        public List<Job> ActiveJob(int UserId)
        {
            var data = (from a in DB.Jobs
                        where a.AssignSeekerId == UserId &&
       a.IsComplete.ToString() == "false"
                        select a).ToList();
            return data;
        }

        //admin chat list
        public List<UserChats> GetUserChatList(int UserId)
        {
            var data = (from a in DB.Chats
                        join um in DB.UserMasters on a.FromId equals um.Id
                        join b in DB.UserMasters on a.ToId equals b.Id
                        where a.FromId == UserId
                        select new UserChats
                        {
                            FromId = a.FromId,
                            ToId = a.ToId,
                            senderName = um.UserName,
                            ReceiverName = b.UserName,
                            JobId = a.JobId
                            // message = a.UserMessage,
                            // MessageDateTime = a.MessageDateTime,
                        }).Distinct().ToList();
            return data;
        }
        public UserMaster GetUserDetails(int UserId)
        {
            var data = (from a in DB.UserMasters where a.Id == UserId select a).FirstOrDefault();
            return data;
        }
        public List<sp_GetUserChatResult> GetUserChats(int FromId, int ToId, int JobId)
        {
            List<sp_GetUserChatResult> userchat = new List<sp_GetUserChatResult>();
            //List<GetUserprofilepic> usrpic = new List<GetUserprofilepic>();

            var Chats = DB.sp_GetUserChat(FromId, ToId, JobId).ToList();
            foreach (var c in Chats)
            {
                c.SenderProfilePic = path + c.SenderProfilePic;
                c.ReceiverProfilePic = path + c.ReceiverProfilePic;
            }
            return Chats;

        }

        ////public List<Job> GetJob()
        // //   public List<Job> GetJob(int Id, string JobTitle, string HowLong, string Amount, string JobCategory, string Location, string FromDate, string JobDescription, string Latitude, string Longitude, int UserId)
        //{
        //    //List<Job> GetJob = new List<Job>();
        //    //Job jobsearchList = new Job();h
        //    //double milos = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        //    ////var d = (DB.Jobs).Where(JobTitle == JobTitle || Amount == Convert.ToDecimal(Amount)).ToList();

        //    //return jobsearchList;


        //}

        public List<GetMyJobList> GetMyJobs(int UserId, double radius, string Latitude, string Longitude)
        {
            double milos = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            List<GetMyJobList> jobList = new List<GetMyJobList>();
            List<sp_GetMyJobResult> data1 = new List<sp_GetMyJobResult>();
            data1 = DB.sp_GetMyJob(radius, Latitude, Longitude).ToList();
            var d = data1.Where(x => x.IsComplete != true
                          && x.IsCancel != true && Convert.ToDouble(x.FromDate) > milos).ToList();

            //var data = (from a in DB.Jobs
            //            where /*a.UserId != UserId &&*/
            //            a.IsComplete.ToString() == "false"
            //            && a.IsCancel.ToString() == "false" && Convert.ToDouble(a.FromDate) > milos
            //            select a).ToList();
            foreach (var s in d)
            {

                var appicationst = (from b in DB.ApplicationStatus
                                    where b.JobId == s.Id && b.ListerId == s.UserId
                                    && b.IsApply.ToString() == "true"
                                    && b.IsAcceptedByLister.ToString() == "true" && b.NotSelected.ToString() == "false"
                                    select b).FirstOrDefault();

                if (appicationst == null)
                {
                    GetMyJobList job = new GetMyJobList();
                    job.Id = s.Id;
                    job.JobTitle = s.JobTitle;
                    job.JobCategory = s.JobCategory;
                    job.JobDescription = s.JobDescription;
                    //  job.JobPicture = path + a.JobPicture;
                    job.Location = s.Location;
                    job.Longitude = s.Longitude;
                    job.Latitude = s.Latitude;
                    job.UserId = s.UserId;
                    job.IsComplete = s.IsComplete == null ? false : s.IsComplete;

                    job.FromDate = s.FromDate;
                    job.CompletedDate = s.CompletedDate == null ? "" : s.CompletedDate;
                    job.Amount = s.Amount;
                    job.CreatedDate = s.CreatedDate;
                    job.HowLong = s.HowLong == null ? "" : s.HowLong;
                    job.Distance = s.distance;
                    var jobImge = (from x in DB.JobImages
                                   where x.JobId == s.Id
                                   select new JobImageList
                                   {
                                       Id = x.Id,
                                       JobId = x.JobId,
                                       UserId = x.UserId,
                                       ImageName = path + x.ImageName

                                   }).FirstOrDefault();
                    var ListerProfilePicture = (from a in DB.UserMasters where a.Id == s.UserId select a.ProfilePicture).FirstOrDefault();
                    if (jobImge != null)
                    {
                        job.JobPicture = jobImge.ImageName;
                    }
                    if (ListerProfilePicture != null)
                    {
                        job.ListerProfilePicture = path + ListerProfilePicture;
                    }
                    jobList.Add(job);
                }
            }
            return jobList.OrderByDescending(x => x.CreatedDate).ToList();
        }
        public List<SearchJobResult> JobSearch(int UserId, string Category, double radius, string latitude, string longitude, string minprice, string maxprice)
        {
            double milos = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            List<sp_GetMyJobResult> j1 = new List<sp_GetMyJobResult>();
            List<sp_GetMyJobResult> j2 = new List<sp_GetMyJobResult>();
            List<sp_GetMyJobResult> j3 = new List<sp_GetMyJobResult>();
            j1 = DB.sp_GetMyJob(radius, latitude, longitude).Where(x => x.IsComplete.ToString() != "true" && Convert.ToDouble(x.FromDate) > milos && x.IsCancel != true).ToList();
            //a.IsComplete.ToString() == "false"
            //&& a.IsCancel.ToString() == "false"
            // j1 = DB.sp_SearchJob(UserId, radius, latitude, longitude).Where(x => x.IsComplete.ToString() != "true" && Convert.ToDouble(x.FromDate) > milos).ToList();
            var data = (from i in DB.ApplicationStatus select i).ToList();
            //var job = (from a in DB.Jobs select a).ToList();
            if (!string.IsNullOrEmpty(Category))
            {


                var Catarray = Category.Split(',');

                foreach (string c_array in Catarray)
                {
                    j2 = new List<sp_GetMyJobResult>();
                    if (c_array != "")
                    {

                        j2 = (from u in j1
                              where u.JobCategory.Contains(c_array)
                              select u).ToList();


                    }
                    j3.AddRange(j2);
                }
            }
            if (!string.IsNullOrEmpty(Category))
            {
                if (!string.IsNullOrEmpty(minprice) && !string.IsNullOrEmpty(maxprice))
                {
                    j1 = (from r in j3
                          where Convert.ToDouble(r.Amount) >= Convert.ToDouble(minprice)
                          && Convert.ToDouble(r.Amount) <= Convert.ToDouble(maxprice)
                          select r).ToList();
                }
                else if (string.IsNullOrEmpty(minprice) && !string.IsNullOrEmpty(maxprice))
                {
                    j1 = (from r in j3
                          where Convert.ToDouble(r.Amount) <= Convert.ToDouble(maxprice)
                          select r).ToList();
                }
                else if (!string.IsNullOrEmpty(minprice) && string.IsNullOrEmpty(maxprice))
                {
                    j1 = (from r in j3
                          where Convert.ToDouble(r.Amount) >= Convert.ToDouble(minprice)
                          select r).ToList();
                }
                else { }
            }
            else
            {
                if (!string.IsNullOrEmpty(minprice) && !string.IsNullOrEmpty(maxprice))
                {
                    j1 = (from r in j1
                          where Convert.ToDouble(r.Amount) >= Convert.ToDouble(minprice)
                          && Convert.ToDouble(r.Amount) <= Convert.ToDouble(maxprice)
                          select r).ToList();
                }
                else if (string.IsNullOrEmpty(minprice) && !string.IsNullOrEmpty(maxprice))
                {
                    j1 = (from r in j1
                          where Convert.ToDouble(r.Amount) <= Convert.ToDouble(maxprice)
                          select r).ToList();
                }
                else if (!string.IsNullOrEmpty(minprice) && string.IsNullOrEmpty(maxprice))
                {
                    j1 = (from r in j1
                          where Convert.ToDouble(r.Amount) >= Convert.ToDouble(minprice)
                          select r).ToList();
                }
                else { }
            }

            //j3.AddRange(j1);
            SearchJobResult newresult = new SearchJobResult();
            List<SearchJobResult> newresultList = new List<SearchJobResult>();


            //else if (Convert.ToDouble(jobdata.FromDate) < milos)

            foreach (var m in j1)
            {
                var data1 = data.Where(x => x.JobId == m.Id).ToList();
                if (data1.Count == 0)
                {
                    newresult = new SearchJobResult();
                    var jobImge = (from a in DB.JobImages
                                   where a.JobId == m.Id
                                   select new JobImageList
                                   {
                                       Id = a.Id,
                                       JobId = a.JobId,
                                       UserId = a.UserId,
                                       ImageName = path + a.ImageName

                                   }).FirstOrDefault();
                    newresult.Id = m.Id;
                    newresult.distance = m.distance;
                    newresult.FromDate = m.FromDate;
                    newresult.Amount = m.Amount;
                    newresult.CreatedDate = m.CreatedDate;
                    newresult.HowLong = m.HowLong;
                    newresult.JobCategory = m.JobCategory;
                    newresult.JobDescription = m.JobDescription;
                    newresult.JobTitle = m.JobTitle;
                    newresult.Latitude = m.Latitude;
                    newresult.Longitude = m.Longitude;
                    newresult.UserId = m.UserId;
                    newresult.Location = m.Location;
                    var ListerProfilePicture = (from a in DB.UserMasters where a.Id == m.UserId select a.ProfilePicture).FirstOrDefault();

                    if (ListerProfilePicture != null)
                    {
                        newresult.ProfilePicture = path + ListerProfilePicture;
                    }
                    // newresult.ProfilePicture = path + m.ProfilePicture;
                    newresult.AmountWithAdminCharges = m.AmountWithAdminCharges;
                    if (jobImge != null)
                    {
                        newresult.JobPicture = jobImge.ImageName;
                    }
                    if (m.AssignSeekerId == UserId && m.IsCancelSeeker == true)
                    {

                    }
                    else
                    {
                        newresultList.Add(newresult);
                    }

                }
                else
                {

                    var data2 = data1.Where(x => x.IsAcceptedByLister == true).FirstOrDefault();
                    if (data2 == null)
                    {
                        newresult = new SearchJobResult();
                        var jobImge = (from a in DB.JobImages
                                       where a.JobId == m.Id
                                       select new JobImageList
                                       {
                                           Id = a.Id,
                                           JobId = a.JobId,
                                           UserId = a.UserId,
                                           ImageName = path + a.ImageName

                                       }).FirstOrDefault();
                        newresult.Id = m.Id;
                        newresult.distance = m.distance;
                        newresult.FromDate = m.FromDate;
                        newresult.Amount = m.Amount;
                        newresult.CreatedDate = m.CreatedDate;
                        newresult.HowLong = m.HowLong;
                        newresult.JobCategory = m.JobCategory;
                        newresult.JobDescription = m.JobDescription;
                        newresult.JobTitle = m.JobTitle;
                        newresult.Latitude = m.Latitude;
                        newresult.Longitude = m.Longitude;
                        newresult.UserId = m.UserId;
                        newresult.Location = m.Location;
                        newresult.AmountWithAdminCharges = m.AmountWithAdminCharges;
                        var ListerProfilePicture = (from a in DB.UserMasters where a.Id == m.UserId select a.ProfilePicture).FirstOrDefault();

                        if (ListerProfilePicture != null)
                        {
                            newresult.ProfilePicture = path + ListerProfilePicture;
                        }
                        //newresult.ProfilePicture = path + m.ProfilePicture;
                        if (jobImge != null)
                        {
                            newresult.JobPicture = jobImge.ImageName;
                        }
                        if (m.AssignSeekerId == UserId && m.IsCancelSeeker == true)
                        {

                        }
                        else
                        {
                            newresultList.Add(newresult);
                        }

                        //newresultList.Add(newresult);
                    }
                }
                //newresult.imglist = jobImge;

            }
            return newresultList.OrderByDescending(x => x.CreatedDate).ToList();
        }
        public List<DisputeResolution> GetDisputeResolution()
        {
            var data = (from a in DB.DisputeResolutions select a).ToList();
            return data;
        }



        public Job CreateJob(int Id, int UserId, string JobTitle, string HowLong, string Amount,
            string JobCategory, string Location, string FromDate, string JobDescription, string Latitude,
            string Longitude, List<string> imglist)
        {
            var AdminCharges = (from jf in DB.JobFees select jf).FirstOrDefault();
            Job j = new Job();
            if (Id > 0)
            {
                j = (from a in DB.Jobs where a.Id == Id select a).FirstOrDefault();
                if (!string.IsNullOrEmpty(UserId.ToString()))
                {
                    j.UserId = UserId;
                }
                if (!string.IsNullOrEmpty(JobTitle))
                {
                    j.JobTitle = JobTitle;
                }
                if (!string.IsNullOrEmpty(HowLong))
                {
                    j.HowLong = HowLong;
                }
                if (!string.IsNullOrEmpty(Amount))
                {

                    j.Amount = Convert.ToDecimal(Amount);
                    decimal jobAmountWithAdminCharges = (j.Amount) * (AdminCharges.JobListerFee / 100) + (j.Amount) ?? 0;

                    j.AmountWithAdminCharges = jobAmountWithAdminCharges;
                }

                if (!string.IsNullOrEmpty(JobCategory))
                {
                    j.JobCategory = JobCategory;
                }
                if (!string.IsNullOrEmpty(Location))
                {
                    j.Location = Location;
                }
                if (!string.IsNullOrEmpty(FromDate))
                {
                    j.FromDate = FromDate;
                }
                if (!string.IsNullOrEmpty(JobDescription))
                {
                    j.JobDescription = JobDescription;
                }
                if (!string.IsNullOrEmpty(Latitude))
                {
                    j.Latitude = Latitude;
                }
                if (!string.IsNullOrEmpty(Longitude))
                {
                    j.Longitude = Longitude;
                }
                //j.CompletedDate = CompletedDate;
                //if (!string.IsNullOrEmpty(JobPicture))
                //{
                //    j.JobPicture = JobPicture;
                //}

                j.ModifyDate = DateTime.Now;

            }
            else
            {
                j.UserId = UserId;
                j.JobTitle = JobTitle;
                j.HowLong = HowLong;
                j.Amount = Convert.ToDecimal(Amount);
                decimal jobAmountWithAdminCharges = (j.Amount) * (AdminCharges.JobListerFee / 100) + (j.Amount) ?? 0;
                j.AmountWithAdminCharges = jobAmountWithAdminCharges;
                j.JobCategory = JobCategory;
                j.Location = Location;
                j.FromDate = FromDate;
                j.JobDescription = JobDescription;
                j.Latitude = Latitude;
                j.Longitude = Longitude;
                //j.CompletedDate = CompletedDate;
                // j.JobPicture = JobPicture;
                j.CreatedDate = DateTime.Now;
                DB.Jobs.InsertOnSubmit(j);

            }

            DB.SubmitChanges();


            JobImage ji = new JobImage();
            if (imglist != null)
            {
                int i = 0;
                foreach (var a in imglist)
                {
                    ji = new JobImage();

                    if (Id > 0)
                    {
                        //for (int i = 0; i < imglist.Count; i++)
                        //{
                        if (i == 0)
                        {

                            var jobImge = (from s in DB.JobImages where s.JobId == j.Id && s.UserId == UserId select s).ToList();
                            if (jobImge.Count > 0)
                            {
                                DB.JobImages.DeleteAllOnSubmit(jobImge);
                                DB.SubmitChanges();
                            }
                            i++;
                        }
                        //}


                        //ji.JobId = JobId;
                        //ji.UserId = UserId;
                        //if (!string.IsNullOrEmpty(a))
                        //{
                        //    ji.ImageName = a;
                        //}

                        //ji.CreatedDate = DateTime.Now;


                    }

                    ji.JobId = j.Id;
                    ji.UserId = UserId;
                    if (!string.IsNullOrEmpty(a))
                    {
                        ji.ImageName = a;
                    }


                    ji.CreatedDate = DateTime.Now;
                    //if (Id == 0)
                    //{
                    DB.JobImages.InsertOnSubmit(ji);
                    //}

                    //}



                    DB.SubmitChanges();
                }

            }
            else
            {
                var jobImge = (from s in DB.JobImages where s.JobId == j.Id && s.UserId == UserId select s).ToList();
                if (jobImge.Count > 0)
                {
                    DB.JobImages.DeleteAllOnSubmit(jobImge);
                    DB.SubmitChanges();
                }
            }
            //send activity notification
            var ListerDetail = (from a in DB.UserMasters where a.Id == UserId select a).FirstOrDefault();
            if (Id == 0)
            {

                string NotificationType = "Activity";

                List<sp_UserListResult> u1 = new List<sp_UserListResult>();
                List<sp_UserListResult> u2 = new List<sp_UserListResult>();
                List<sp_UserListResult> u3 = new List<sp_UserListResult>();
                u1 = DB.sp_UserList(100, Latitude, Longitude).ToList();
                if (!string.IsNullOrEmpty(JobCategory))
                {


                    var Catarray = JobCategory.Split(',');

                    foreach (string c_array in Catarray)
                    {
                        u2 = new List<sp_UserListResult>();
                        if (c_array != "")
                        {

                            u2 = (from u in u1
                                  where u.JobType.Contains(c_array)
                                  select u).ToList();


                        }
                        u3.AddRange(u2);
                    }
                }
                foreach (var d in u3)
                {
                    if (d.DeviceType.ToUpper() == "ANDROID")
                    {
                        SendNotification(d.DeviceId, UserId.ToString(), d.Id.ToString(), d.FirstName + " " + d.LastName, ListerDetail.FirstName + ", a new job " + JobTitle + "has just been posted. Open the Azzida app to view new jobs near you.", Id, NotificationType, ListerDetail.FirstName + " " + ListerDetail.LastName, path + ListerDetail.ProfilePicture, "");
                    }
                    else
                    {
                        string otherparam = "\"FromUserId\":\"" + UserId + "\",\"toUserId\":\"" + d.Id + "\",\"fullName\":\"" + d.FirstName + " " + d.LastName + "\",\"JobId\":\"" + Id + "\",\"NotificationType\":\"" + NotificationType + "\",\"SenderFullName\":\"" + ListerDetail.FirstName + " " + ListerDetail.LastName + "\",\"SenderProfilePicture\":\"" + path + ListerDetail.ProfilePicture + "\" ";
                        string message = ListerDetail.FirstName + ", a new job " + JobTitle + "has just been posted. Open the Azzida app to view new jobs near you.";
                        SendIhpone(d.DeviceId, otherparam, message);
                    }

                    string str = "<div><h2 style='color:blue !important'><img src='http://13.72.77.167:8085/ApplicationImages/LogoAzzida.png'/></h2>";
                    //  str = str + "<h5 style='color:blue !important;'>" + "Start Getting Stuff Done!" + " </h5></br>";
                    str = str + "<p style='color:black !important;'>Dear " + ListerDetail.FirstName + " " + ListerDetail.LastName + ", </p>";
                    str = str + "<p style='color:black !important;'> create a job matching your interest. </p>";
                    // str = str + "<p style='color:black !important;'>  create a job matching your interest. Click here to confirm " + "<a href='" + applink + "' style='color:blue !important;'>" + applink + "</a> </p></br>";
                    //  str = str + "<p><a href='https://www.facebook.com/azzidajobs/' style='color:black !important;'>Follow us on Facebook</a></p></br>";
                    str = str + "<p style='color:black !important;margin-block: 0px !important;'>The Team At Azzida</p>";
                    str = str + "<p style='color:black !important;margin-block: 0px !important;'>Email: <a href='mailto:support@azzida.com' style='color:blue !important;'>support@azzida.com</a></p>";
                    str = str + "<p style='color:black !important;margin-block: 0px !important;'>AppLink: <a href='https://play.google.com/store/apps/details?id=com.azzida' style='color:blue !important;'>https://play.google.com/store/apps/details?id=com.azzida</a></p>";
                    str = str + "<p style='margin-block:0px !important;'><a href='https://www.instagram.com/azzida_app' style='color:blue !important;'>Follow us on Instagram</a></p>";
                    str = str + "<p style='color:black !important;margin-block:0px !important;'>Web: <a href='http://www.azzida.com/' style='color:blue !important;'>www.Azzida.com</a></p>";
                    //str = str + "<p style='color:black !important;margin-block:0px !important;'>Email: <a href='mailto:support@azzida.com' style='color:blue !important;'>support@azzida.com</a></p>";
                    str = str + "</div>";
                    SendEMail("noreply@azzida.com", d.UserEmail, NotificationType, str);
                    // SendEMail("noreply@azzida.com", d.UserEmail, NotificationType, ListerDetail.FirstName + " " + ListerDetail.LastName + " create a job matching your interest.");
                }
            }
            return j;
        }


        public Job JobComplete(int Id, string CompleteDate, string IsComplete)
        {
            var data = (from a in DB.Jobs where a.Id == Id select a).FirstOrDefault();
            if (data != null)
            {
                data.CompletedDate = CompleteDate;
                data.IsComplete = Convert.ToBoolean(IsComplete);
                DB.SubmitChanges();
            }
            return data;
        }
        public JobCategory CreateJobCategory(int Id, string CategoryName)
        {
            JobCategory jc = new JobCategory();
            if (Id > 0)
            {
                jc = (from a in DB.JobCategories where a.Id == Id select a).FirstOrDefault();
                jc.CategoryName = CategoryName;

            }
            else
            {
                jc.CategoryName = CategoryName;
                DB.JobCategories.InsertOnSubmit(jc);
            }
            DB.SubmitChanges();
            return jc;
        }

        //get applink
        public Job GetAppLink(int Id, string applink)
        {
            var data = (from a in DB.Jobs where a.Id == Id select a).FirstOrDefault();
            if (data != null)
            {
                data.Applink = applink;
                DB.SubmitChanges();

            }
            return data;
        }

        public List<JobCategory> GetJobCategory()
        {
            var data = (from a in DB.JobCategories select a).OrderBy(x => x.CategoryName).ToList();
            return data;
        }

        public JobCategory GetJobCategoryById(int Id)
        {
            var data = (from a in DB.JobCategories where a.Id == Id select a).FirstOrDefault();
            return data;
        }

        public void DeleteCategoryById(int Id)
        {
            var data = (from a in DB.JobCategories where a.Id == Id select a).FirstOrDefault();
            DB.JobCategories.DeleteOnSubmit(data);
            DB.SubmitChanges();
        }


        public DisputeResolution SaveDispute(int Id, int UserId, int JobId,
            string DisputeReason, string PostAssociate,/* string ContactWay,*/
            string Description, string Attachment)
        {
            DisputeResolution dr = new DisputeResolution();
            if (Id > 0)
            {
                dr = (from a in DB.DisputeResolutions where a.Id == Id select a).FirstOrDefault();
                dr.UserId = UserId;
                dr.JobId = JobId;
                dr.DisputeReason = DisputeReason;
                dr.PostAssociate = PostAssociate;
                //dr.ContactWay = ContactWay;
                dr.Description = Description;
                if (Attachment != "")
                {
                    dr.Attachment = Attachment;
                }

                dr.ModifyDate = DateTime.Now;
            }
            else
            {
                dr.UserId = UserId;
                dr.JobId = JobId;
                dr.DisputeReason = DisputeReason;
                dr.PostAssociate = PostAssociate;
                //dr.ContactWay = ContactWay;
                dr.Description = Description;
                dr.Attachment = Attachment;
                dr.CreatedDate = DateTime.Now;
                DB.DisputeResolutions.InsertOnSubmit(dr);
            }
            DB.SubmitChanges();


            //string str = "";

            //str = str + "<div><p style='color:black !important'>" + "Hi, " + "</p>";



            string str = "<div><h2 style='color:blue !important'><img src='http://13.72.77.167:8085/ApplicationImages/LogoAzzida.png'/></h2>";
            //  str = str + "<h5 style='color:blue !important;'>" + "Start Getting Stuff Done!" + " </h5></br>";
            str = str + "<p style='color:black !important;'>Hi, </p>";
            str = str + "<p style='color:black !important'>" + "Check Dispute from Id=" + dr.Id + "</p></br>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>The Team At Azzida</p>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>Email: <a href='mailto:support@azzida.com' style='color:blue !important;'>support@azzida.com</a></p>";
            str = str + "<p style='color:black !important;margin-block: 0px !important;'>AppLink: <a href='https://play.google.com/store/apps/details?id=com.azzida' style='color:blue !important;'>https://play.google.com/store/apps/details?id=com.azzida</a></p>";
            str = str + "<p style='margin-block:0px !important;'><a href='https://www.instagram.com/azzida_app' style='color:blue !important;'>Follow us on Instagram</a></p>";
            str = str + "<p style='color:black !important;margin-block:0px !important;'>Web: <a href='http://www.azzida.com/' style='color:blue !important;'>www.Azzida.com</a></p>";
            //str = str + "<p style='color:black !important;margin-block:0px !important;'>Email: <a href='mailto:support@azzida.com' style='color:blue !important;'>support@azzida.com</a></p>";
            str = str + "</div>";


            SendEMail("noreply@azzida.com", "support@azzida.com", "Check Dispute", str);
            return dr;
        }
        //make dispute payment
        //public Payment CreateDisputePayment(int JobId, int UserId, string CustomerId, decimal Tax, decimal Amount, decimal TotalAmount, string PaymentToken, string PaymentType)
        //{
        //    var chargeService = new StripeChargeService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);



        //    //  var paymentId = new StripeChargeService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);
        //    if (CustomerId != null)
        //    {
        //        var myCharge2 = new StripeChargeCreateOptions
        //        {
        //            Amount = Convert.ToInt32(TotalAmount) * 100,
        //            Currency = "usd",
        //            Description = "Job payment",
        //            CustomerId = CustomerId,
        //            //SourceTokenOrExistingSourceId = token,
        //            Capture = true,
        //            TransferGroup = JobId.ToString()

        //        };
        //        paymentId = chargeService.Create(myCharge2);
        //    }
        //    else
        //    {
        //        var myCharge = new StripeChargeCreateOptions
        //        {
        //            Amount = Convert.ToInt32(TotalAmount * 100),
        //            Currency = "usd",
        //            Description = "Job Payment",
        //            //CustomerId = custid,
        //            SourceTokenOrExistingSourceId = PaymentToken,
        //            Capture = true
        //        };
        //        paymentId = chargeService.Create(myCharge);
        //    }


        //    Payment p = new Payment();
        //    p.JobId = JobId;
        //    p.UserId = UserId;
        //    p.Tax = Tax;
        //    p.Amount = Amount;
        //    p.TotalAmount = TotalAmount;
        //    // p.Rate = Rate;
        //    //p.WorkDuration = WorkDuration;
        //    p.PaymentToken = PaymentToken;
        //    p.PaymentId = paymentId.Id;
        //    if (paymentId.Paid == true)
        //    {
        //        p.IsSuccess = true;
        //    }
        //    else
        //    {
        //        p.IsSuccess = false;
        //    }
        //    p.PaymentType = PaymentType;
        //    p.CreatedDate = Convert.ToString(DateTime.Now);
        //    DB.Payments.InsertOnSubmit(p);
        //    DB.SubmitChanges();


        //    return p;
        //}

        public List<RoleMaster> GetRoles()
        {
            var data = (from a in DB.RoleMasters select a).ToList();
            return data;
        }

        public void NotificationFailed(ApnsNotification notification, AggregateException aggregateEx)
        {
            //Do something here
            //  WriteLog("pass");
            aggregateEx.Handle(ex =>
            {

                // See what kind of exception it was to further diagnose
                if (ex is ApnsNotificationException)
                {
                    var notificationException = (ApnsNotificationException)ex;

                    // Deal with the failed notification
                    var apnsNotification = notificationException.Notification;
                    var statusCode = notificationException.ErrorStatusCode;



                }
                else
                {
                    // Inner exception might hold more useful information like an ApnsConnectionException           
                    // Console.WriteLine ($"Apple Notification Failed for some unknown reason : {ex.InnerException}");
                }

                // Mark it as handled

                return true;
            });


        }
        public void NotificationSucceeded(ApnsNotification notification)
        {
            //Do something here
            //WriteLog("pass");
        }
        public void SendIhpone(string token, string otherparam, string message)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            // Configuration (NOTE: .pfx can also be used here)
            var config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox,
              System.Web.HttpContext.Current.Server.MapPath("Azzida.p12"), "123456");
            //config.ValidateServerCertificate = false;
            // Create a new broker
            var apnsBroker = new ApnsServiceBroker(config);

            // Wire up events
            apnsBroker.OnNotificationFailed += NotificationFailed;

            apnsBroker.OnNotificationSucceeded += NotificationSucceeded;

            // Start the broker
            apnsBroker.Start();


            // Queue a notification to send
            apnsBroker.QueueNotification(new ApnsNotification
            {
                DeviceToken = token,
                Payload = JObject.Parse("{\"aps\":{        \"alert\" :  \"" + message + "\" ,  \"badge\":7 , \"sound\":\"default\" , \"content-available\" : 1}, " + otherparam + " }"),

            });



        }

        //connect account

        public string PayoutToConnectedAccount(decimal amount, string accountnumber, int UserId)
        {
            string str = "";
            try
            {
                var ToUserAccountId = (from a in DB.UserMasters where a.Id == UserId select a).FirstOrDefault();
                // var jobAmount = (from j in DB.Jobs where j.Id == JobId select j).FirstOrDefault();
                var fee = (from f in DB.JobFees select f).FirstOrDefault();

                var accountService1 = new StripeAccountService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);
                //"sk_test_QS7zwe52WufPIZHXjwnpgj1D");
                // StripeAccount account = accountService.Get("acct_19unI3J1Y72ANlMu");
                //acct_1CHlgSIoFMBSbD2s


                var ddd = new StripeTransferService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);
                //  bidamount = bidamount - (bidamount * Convert.ToDecimal(0.10));
                // decimal paymentAmount = (jobAmount.Amount) - (jobAmount.Amount) * (fee.JobSeekerFee / 100) ?? 0;
                decimal paymentAmount = Convert.ToDecimal(amount) - Convert.ToDecimal(amount) * (fee.JobSeekerFee / 100) ?? 0;


                if (!string.IsNullOrEmpty(ToUserAccountId.StripeAccId))
                {
                    var myCharge112 = new StripeTransferCreateOptions
                    {
                        //Amount = Convert.ToInt32(paymentAmount * Convert.ToDecimal(100)),
                        Amount = Convert.ToInt32(amount * 100),
                        Currency = "usd",
                        //SourceTransaction =d.FirstOrDefault().TokenId,  // "txn_1CNImMIoFMBSbD2sPhE8O8co",
                        Destination = ToUserAccountId.StripeAccId,//"acct_179XbqHpdOej4gWA",
                                                                  // TransferGroup = jobid.ToString(),
                                                                  // SourceType = "card"
                                                                  //TransferGroup = JobId.ToString()

                    };
                    StripeTransfer stripeCharge11 = ddd.Create(myCharge112);

                    //if (paymentData != null)
                    //{
                    //    paymentData.SeekerPaymentAmount = paymentAmount;
                    //    paymentData.ModifyDate = System.DateTime.Now.ToString();
                    //    paymentData.IsSeekerPaymentDone = true;
                    //    DB.SubmitChanges();


                    //    var tippaydata = (from ti in DB.Payments where ti.JobId == paymentData.JobId && ti.PaymentType.ToLower() == "tip" select ti).FirstOrDefault();
                    //    if (tippaydata != null)
                    //    {
                    //        tippaydata.IsSeekerPaymentDone = true;
                    //        DB.SubmitChanges();
                    //    }

                    //}
                }
                //string str = "Success";
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            if (string.IsNullOrEmpty(str))
            {

                string str1 = "";

                ////decimal paymentAmount = Convert.ToDecimal(amount) - Convert.ToDecimal(amount) * (fee.JobSeekerFee / 100) ?? 0;
                try
                {

                    var accountService = new StripePayoutService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"]);

                    var myCharge12 = new StripePayoutCreateOptions
                    {
                        Amount = Convert.ToInt32(amount * 100),

                        Currency = "usd"

                    };
                    var reqoption = new Stripe.StripeRequestOptions
                    {
                        StripeConnectAccountId = accountnumber
                    };
                    var stripeCharge1 = accountService.Create(myCharge12, reqoption);

                    var data = (from a in DB.UserMasters where a.Id == UserId select a).FirstOrDefault();
                    if (amount >= data.UserReceivedAmount)
                    {
                        data.UserReceivedAmount = (data.UserReceivedAmount == null ? 0 : data.UserReceivedAmount) - amount;
                        DB.SubmitChanges();
                    }
                }
                catch (Exception ex)
                {
                    str1 = ex.Message;
                }
                if (!string.IsNullOrEmpty(str1))
                {
                    str = str1;
                }
            }
            return str;
        }
        //retrive account
        public string RetrieveStripeAccount(string code, int userid, string accountnumber)
        {


            string actid = "";
            if (!string.IsNullOrEmpty(accountnumber))
            {
                // var service = new Stripe.StripeAccountService(System.Configuration.ConfigurationManager.AppSettings["StripeToken"].ToString());
                //var d= service.Get(accountnumber);
                // actid = d.Id;
                actid = accountnumber;
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    ServicePointManager.DefaultConnectionLimit = 9999;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.stripe.com/v1/account_links");
                    request.Method = "POST";
                    request.Accept = "application/json";
                    request.Headers.Add("Authorization", "Basic " + System.Configuration.ConfigurationManager.AppSettings["StripeToken"].ToString());

                    request.UserAgent = "curl/7.37.0";
                    request.ContentType = "application/x-www-form-urlencoded";
                    string param = "account=" + actid + "&refresh_url=https://example.com/reauth&return_url=https://example.com/return&type=account_onboarding";
                    if (param != "")
                    {
                        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            string data = param;
                            streamWriter.Write(data);
                        }
                    }
                    string text;
                    try
                    {
                        var response = request.GetResponse();
                        using (var sr = new StreamReader(response.GetResponseStream()))
                        {
                            text = sr.ReadToEnd();

                        }

                    }
                    catch (WebException ex)
                    {
                        try
                        {
                            var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                            text = resp;

                        }
                        catch (Exception exmain)
                        {
                            text = exmain.Message;

                        }
                    }
                }
                catch { }
            }
            else
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.DefaultConnectionLimit = 9999;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://connect.stripe.com/oauth/token");
                request.Method = "POST";
                request.Accept = "application/json";
                // request.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["APIKEY"], "");
                request.UserAgent = "curl/7.37.0";
                request.ContentType = "application/x-www-form-urlencoded";
                string param = "client_secret=" + System.Configuration.ConfigurationManager.AppSettings["StripeToken"].ToString() + "&code=" + code + "&grant_type=authorization_code";
                if (param != "")
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        string data = param;
                        streamWriter.Write(data);
                    }
                }
                string text;
                try
                {
                    var response = request.GetResponse();
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        text = sr.ReadToEnd();

                    }
                    RetrieveStripeAccountClass myDeserializedClass = Newtonsoft.Json.JsonConvert.DeserializeObject<RetrieveStripeAccountClass>(text);
                    actid = myDeserializedClass.stripe_user_id;
                }
                catch (WebException ex)
                {
                    try
                    {
                        var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                        text = resp;

                    }
                    catch (Exception exmain)
                    {
                        text = exmain.Message;

                    }
                }


            }
            UserMaster um = (from t in DB.UserMasters where t.Id == userid select t).FirstOrDefault();
            um.StripeAccId = actid;
            DB.SubmitChanges();
            return actid;
        }
    }

    public class GetLoginDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public int? RoleId { get; set; }
        public string Skills { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string TokenId { get; set; }
        public string UserName { get; set; }
        public string VerifiedId { get; set; }
        public string Provider { get; set; }
        public string JobType { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsActive { get; set; }
        public string GoogleEmail { get; set; }
        public string FaceBookEmail { get; set; }
        public string EmailType { get; set; }
        public string DeviceType { get; set; }
        public string RefCode { get; set; }
        public string StripeAccId { get; set; }
        public bool AzzidaVerified { get; set; }
        public string DeviceId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal receivedAmount { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class RetrieveStripeAccountClass
    {
        public string access_token { get; set; }
        public bool livemode { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public string stripe_publishable_key { get; set; }
        public string stripe_user_id { get; set; }
        public string scope { get; set; }
    }
    public class AccountCapabilitiesTransfersOptions
    {
        public bool Requested { get; set; }
    }

    public class AccountCapabilitiesCardPaymentsOptions
    {
        public bool Requested { get; set; }
    }

    public class AccountCapabilitiesOptions
    {
        public object CardPayments { get; set; }
        public object Transfers { get; set; }
    }

    public class AccountCreateOptions
    {
        public string Type { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public object Capabilities { get; set; }
    }

    public class TipSuccessData
    {
        public int Id { get; set; }
        public int? JobId { get; set; }
        public int? UserId { get; set; }
        public int? SeekerId { get; set; }
        public decimal? SeekerRate { get; set; }
        public decimal? TippingAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Status { get; set; }
        public int paymentId { get; set; }
    }

    public class PaymentDetails
    {
        public int Id { get; set; }
        public int? JobId { get; set; }
        public int? UserId { get; set; }
        public decimal? RefBalance { get; set; }
        public int? ToUserId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TotalAmount { get; set; }
        public string CreatedDate { get; set; }
        public string PaymentToken { get; set; }
        public string PaymentType { get; set; }
        public string paymentId { get; set; }
        public bool? IsSuccess { get; set; }
    }

    public class PaymentHistoryData
    {
        public int JobId { get; set; }
        public decimal? JobAmount { get; set; }
        public int? ListerId { get; set; }
        public string ListerName { get; set; }
        public int? SeekerId { get; set; }
        public string SeekerName { get; set; }
        public decimal? TotalAmount { get; set; }
        public string JobTitle { get; set; }
        public DateTime? PaymentTime { get; set; }
        public bool? IsComplete { get; set; }
        public bool? IscompleteUser { get; set; }
        public string Status { get; set; }
        public bool? IsSeekerPaymentDone { get; set; }
        public bool? IsListerPaymentDone { get; set; }
        public string paymentType { get; set; }
    }

    public class UserChats
    {
        public int? FromId { get; set; }
        public int? ToId { get; set; }
        public string senderName { get; set; }
        public string ReceiverName { get; set; }
        public string message { get; internal set; }
        public string MessageDateTime { get; internal set; }
        public int? JobId { get; internal set; }
    }

    public class RecentActivity
    {
        public List<postJobs> post = new List<postJobs>();
        public List<appliedJob> applied = new List<appliedJob>();
    }

    public class postJobs
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobCategory { get; set; }
        public string JobDescription { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public decimal? Amount { get; set; }
        public string Location { get; set; }
        public string Howlong { get; set; }
        public int? UserId { get; set; }
        public string FromDate { get; set; }
        public DateTime? CreatedDate { get; internal set; }
    }

    public class appliedJob
    {
        public int Id { get; set; }
        public int? ApplierId { get; set; }
        public string JobTitle { get; set; }
        public string JobCategory { get; set; }
        public string JobDescription { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public decimal? Amount { get; set; }
        public string Location { get; set; }
        public string Howlong { get; set; }
        public int? UserId { get; set; }
        public string FromDate { get; set; }
        public DateTime? CreatedDate { get; internal set; }
    }

    public class SaveCardResponse
    {
        public string error;

        public List<UserCard> usercards { get; internal set; }
    }

    public class GetPorfileDetail : UserMaster
    {
        public double UserRatingAvg { get; set; }
        public decimal? Balance { get; set; }
        public decimal receivedAmount { get; set; }
    }

    public class SenderData
    {
        public string SenderFullName { get; set; }
        public string UserName { get; set; }
        public string SenderProfilePicture { get; internal set; }
    }

    public class ReceiverData
    {
        public string DeviceId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string DeviceType { get; set; }
        public string UserEmail { get; set; }
        public string FirstName { get; set; }
    }

    public class postAssociate
    {
        public string postassociate { get; set; }
        public decimal? Amount { get; set; }
        public int Id { get; set; }
    }

    public class InProgressJob
    {
        public int JobId { get; set; }

        public string JobDescription { get; set; }
        public string JobPicture { get; set; }
        public string FromDate { get; set; }
        public string HowLong { get; set; }
        public decimal? Amount { get; set; }
        public int ApplicantCount { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string JobTitle { get; set; }
        public int? ListerId { get; internal set; }
        public bool? IsApply { get; set; }
        public bool? ApplicationAccepted { get; set; }
        public bool? OfferAccepted { get; set; }
        public int? SeekerId { get; set; }
        public string ListerName { get; set; }
        public string ListerProfilePicture { get; set; }
        public int ListerCompleteJob { get; set; }
        public bool? IsComplete { get; set; }

        public List<JobImageList> imageList { get; set; }
        public string JobCategory { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ApplicationModifyDate { get; set; }
    }

    public class GetMyJobList
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobCategory { get; set; }
        public string JobDescription { get; set; }
        public string JobPicture { get; set; }
        public string Location { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int? UserId { get; set; }
        public bool? IsComplete { get; set; }
        public string FromDate { get; set; }
        public string CompletedDate { get; set; }
        public decimal? Amount { get; set; }
        //  public bool ApplicationAccepted { get; set; }
        // public bool offerAccecpted { get; set; }
        //   public bool IsApplied { get; set; }
        public string HowLong { get; set; }
        public DateTime? CreatedDate { get; set; }
        public double? Distance { get; set; }
        public string ListerProfilePicture { get; set; }
    }

    public class JobImageList
    {
        public int Id { get; set; }
        public int? JobId { get; set; }
        public int? UserId { get; set; }
        public string ImageName { get; set; }
    }

    public class JobCancel
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobCategory { get; set; }
        public string JobDescription { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Location { get; set; }
        public string FromDate { get; set; }
        public string HowLong { get; set; }

        public string Status { get; set; }
        public decimal? Amount { get; set; }
        public int? AssignSeekerId { get; set; }
        public DateTime? CancelDate { get; internal set; }
    }

    public class ViewPayment
    {
        public List<ViewSender> SenderList { get; set; }
        public List<ViewReceiver> ReceiverList { get; set; }
    }

    public class ViewReceiver
    {
        public int Id { get; set; }

        public int? JobId { get; set; }
        public string JobTitle { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string SenderProfilePicture { get; set; }
        public int? ReceivedFrom { get; internal set; }
        public int? MyId { get; internal set; }
        public string SenderName { get; internal set; }
    }

    public class GetApplicantList
    {
        public int Id { get; set; }
        public int ListerId { get; set; }
        public int SeekerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }

        public int JobCompleteCount { get; internal set; }
    }

    public class SearchJobResult : sp_SearchJobResult
    {
        //public List<JobImageList> imglist { get; set; }
    }

    public class JobDetail
    {
        public int JobId { get; set; }

        public string JobDescription { get; set; }
        public string JobPicture { get; set; }
        public string FromDate { get; set; }
        public string HowLong { get; set; }
        public decimal? Amount { get; set; }
        public int ApplicantCount { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string JobTitle { get; set; }
        public int? ListerId { get; set; }
        public bool? IsApply { get; set; }
        public bool? ApplicationAccepted { get; set; }
        public bool? OfferAccepted { get; set; }
        public int? SeekerId { get; set; }
        public string ListerName { get; set; }
        public string ListerProfilePicture { get; set; }
        public int ListerCompleteJob { get; set; }
        public bool? IsComplete { get; set; }

        public string Status { get; set; }
        public List<JobImageList> imageList { get; set; }
        public string JobCategory { get; set; }
        public string SeekerName { get; set; }
        public string Seekerimage { get; set; }
        public string CompletionDate { get; set; }
        public decimal? AmountWithAdminCharges { get; set; }
        public int PymntId { get; internal set; }
        public int TipId { get; internal set; }
        public bool? AzzidaVarified { get; set; }
        public bool? IsJobComplete { get; internal set; }
    }

    public class GetUserJob
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobCategory { get; set; }
        public string JobDescription { get; set; }
        public string JobPicture { get; set; }
        public string Location { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int? UserId { get; set; }
        public bool? IsComplete { get; set; }
        public string FromDate { get; set; }
        public string CompletedDate { get; set; }
        public decimal? Amount { get; set; }
        public string HowLong { get; set; }
        public bool ApplicationAccept { get; set; }
        public bool OfferAccept { get; set; }

        public List<JobImageList> imglist { get; set; }
        //public JobImageList img { get; set; }
    }

    public class ViewSender
    {
        public int Id { get; set; }
        public int? ListerId { get; set; }
        public int? PaidTo { get; set; }
        public int? JobId { get; set; }
        public string JobTitle { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? CreatedDate { get; set; }

        public int? ReceivedFrom { get; set; }
        public string SenderName { get; set; }
        public string SenderProfilePicture { get; set; }
        public int? MyId { get; set; }
        public string ToName { get; set; }
        public string ToProfilePicture { get; set; }
        public decimal? SeekerPaymentAmount { get; set; }
        public string PaymentType { get; set; }
        public bool IsListerPaymentDone { get; internal set; }
        public bool IsSeekerPaymentDone { get; internal set; }
        public decimal? JobAmount { get; internal set; }
        public decimal? FeesPaid { get; internal set; }
    }
    public class GetMyJob
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobCategory { get; set; }
        public string JobDescription { get; set; }
        public string JobPicture { get; set; }
        public string Location { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int? UserId { get; set; }
        public bool? IsComplete { get; set; }
        public string FromDate { get; set; }
        public string CompletedDate { get; set; }
        public decimal? Amount { get; set; }
        public bool ApplicationAccepted { get; set; }
        public bool offerAccecpted { get; set; }
        public bool IsApplied { get; set; }
        public string HowLong { get; set; }
        public JobImageList imglist { get; set; }
    }

    public class ViewListerUser
    {
        public List<Job> Getrecentactivity { get; set; }
        //public int Id { get; set; }
        public int JobCompleteCount { get; set; }
        public int JobPostingcount { get; internal set; }
        public DateTime? joinDate { get; set; }
        public int ListerId { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public bool? AzzidaVerified { get; set; }
    }



    public class GetApplicantDetail
    {
        public string SeekerName { get; set; }
        public List<Job> GetRecentActivity { get; set; }
        //public int Id { get; set; }
        public int JobCompleteCount { get; set; }
        public DateTime? Joindate { get; set; }
        public string LName { get; set; }
        public string profilePicture { get; set; }
        public string UserProfile { get; internal set; }
        public int RatingUserCount { get; set; }
        public int SeekerId { get; set; }
        public string SkillExperience { get; set; }
        public double RateAvg { get; set; }
        public bool? AzzidaVarified { get; internal set; }
    }
}


public class AndroidFCMPushNotificationStatus
{
    public bool Successful
    {
        get;
        set;
    }

    public string Response
    {
        get;
        set;
    }
    public Exception Error
    {
        get;
        set;
    }

}