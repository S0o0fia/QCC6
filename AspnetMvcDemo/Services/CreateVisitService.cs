using AspnetMvcDemo.Models;
using AspnetMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Discovery;

namespace AspnetMvcDemo.Services
{
    public class CreateVisitService
    {
        VisitService visitServ;
        UserService userServ;
        FactoryService factoryServ;
        DateTime dateTime;
        bool result1;
        bool result2;
        bool result3;
        bool result4;
        List<User> Users;

        public CreateVisitService ()
        {
            visitServ = new VisitService();
            userServ = new UserService();
            factoryServ = new FactoryService();
          
        }
        
        public bool CreateVisit()
        {
            var Count = 0;
            dateTime = new DateTime(2020, 5, 1);
            var queryResult = new List<Visitviewmodel>();
            Users = userServ.UserDetails();
            while (Count < 9)
            {
                queryResult = visitServ.GetVisits();
                if (queryResult.Count == 0)
                {

                    autoCreate(dateTime);
                    return true;
                }
                else
                {
                   dateTime = queryResult.LastOrDefault().visitDate;
                    dateTime = dateTime.AddDays(1);
                    autoCreate(dateTime);
                    return false;
                }
                Count += 1;
            }
            return false;
        }

        private void autoCreate (DateTime dateTime)
        {
            var countdays = 1;
            while (countdays < 9)
            {
                //if day is Thursday
                if ((int)dateTime.DayOfWeek == 4)
                {
                    dateTime = dateTime.AddDays(2);
                }
                //if day is Friday
                else if ((int)dateTime.DayOfWeek == 5)
                {
                    dateTime = dateTime.AddDays(1);
                }

                foreach (var user in Users)
                {
                    var fact = factoryServ.GetUsersFactoryID(user.Location_Id);
                    switch (countdays)
                    {
                        case 1:
                            {
                                //to handel the length of the array
                                if (fact.Length >= 0)
                                {
                                    result1 = visitServ.CreateVisits(new VisitDetail
                                    {
                                        FactoryId = fact[0],
                                        MonitorId = user.Id,
                                        VisitDate = dateTime
                                    });
                                    if (result1 == true && fact.Length > 1)
                                    {
                                        result2 = visitServ.CreateVisits(new VisitDetail
                                        {
                                            FactoryId = fact[1],
                                            MonitorId = user.Id,
                                            VisitDate = dateTime
                                        });
                                        if (result2 == true && fact.Length > 2)
                                        {
                                            result3 = visitServ.CreateVisits(new VisitDetail
                                            {
                                                FactoryId = fact[2],
                                                MonitorId = user.Id,
                                                VisitDate = dateTime
                                            });

                                        }
                                    }
                                }
                                break;
                            }
                        case 2:
                            {
                                if (fact.Length > 3)
                                {
                                    result1 = visitServ.CreateVisits(new VisitDetail
                                    {
                                        FactoryId = fact[3],
                                        MonitorId = user.Id,
                                        VisitDate = dateTime
                                    });
                                    if (result1 == true && fact.Length > 4)
                                    {
                                        result2 = visitServ.CreateVisits(new VisitDetail
                                        {
                                            FactoryId = fact[4],
                                            MonitorId = user.Id,
                                            VisitDate = dateTime
                                        });
                                        if (result2 == true && fact.Length > 5)
                                        {
                                            result3 = visitServ.CreateVisits(new VisitDetail
                                            {
                                                FactoryId = fact[5],
                                                MonitorId = user.Id,
                                                VisitDate = dateTime
                                            });
                                        }
                                    }
                                }
                                break;
                            }

                        case 3:
                            {
                                if (fact.Length > 6)
                                {
                                    result1 = visitServ.CreateVisits(new VisitDetail
                                    {
                                        FactoryId = fact[6],
                                        MonitorId = user.Id,
                                        VisitDate = dateTime
                                    });
                                    if (result1 == true && fact.Length > 7)
                                    {
                                        result2 = visitServ.CreateVisits(new VisitDetail
                                        {
                                            FactoryId = fact[7],
                                            MonitorId = user.Id,
                                            VisitDate = dateTime
                                        });
                                        if (result2 == true && fact.Length > 8)
                                        {
                                            result3 = visitServ.CreateVisits(new VisitDetail
                                            {
                                                FactoryId = fact[8],
                                                MonitorId = user.Id,
                                                VisitDate = dateTime
                                            });
                                        }
                                    }
                                }
                                break;
                            }

                        case 4:
                            {
                                if (fact.Length > 9)
                                {
                                    result1 = visitServ.CreateVisits(new VisitDetail
                                    {
                                        FactoryId = fact[9],
                                        MonitorId = user.Id,
                                        VisitDate = dateTime
                                    });
                                    if (result1 == true && fact.Length > 10)
                                    {
                                        result2 = visitServ.CreateVisits(new VisitDetail
                                        {
                                            FactoryId = fact[10],
                                            MonitorId = user.Id,
                                            VisitDate = dateTime
                                        });
                                        if (result2 == true && fact.Length > 11)
                                        {
                                            result3 = visitServ.CreateVisits(new VisitDetail
                                            {
                                                FactoryId = fact[11],
                                                MonitorId = user.Id,
                                                VisitDate = dateTime
                                            });
                                        }
                                    }
                                }
                                break;
                            }

                        case 5:
                            {
                                if (fact.Length > 12)
                                {
                                    result1 = visitServ.CreateVisits(new VisitDetail
                                    {
                                        FactoryId = fact[12],
                                        MonitorId = user.Id,
                                        VisitDate = dateTime
                                    });
                                    if (result1 == true && fact.Length > 13)
                                    {
                                        result2 = visitServ.CreateVisits(new VisitDetail
                                        {
                                            FactoryId = fact[13],
                                            MonitorId = user.Id,
                                            VisitDate = dateTime
                                        });
                                        if (result2 == true && fact.Length > 14)
                                        {
                                            result3 = visitServ.CreateVisits(new VisitDetail
                                            {
                                                FactoryId = fact[14],
                                                MonitorId = user.Id,
                                                VisitDate = dateTime
                                            });
                                        }
                                    }
                                }
                                break;
                            }

                        case 6:
                            {
                                if (fact.Length > 15)
                                {
                                    result1 = visitServ.CreateVisits(new VisitDetail
                                    {
                                        FactoryId = fact[15],
                                        MonitorId = user.Id,
                                        VisitDate = dateTime
                                    });
                                    if (result1 == true && fact.Length > 16)
                                    {
                                        result2 = visitServ.CreateVisits(new VisitDetail
                                        {
                                            FactoryId = fact[16],
                                            MonitorId = user.Id,
                                            VisitDate = dateTime
                                        });
                                        if (result2 == true && fact.Length > 17)
                                        {
                                            result3 = visitServ.CreateVisits(new VisitDetail
                                            {
                                                FactoryId = fact[17],
                                                MonitorId = user.Id,
                                                VisitDate = dateTime
                                            });
                                        }
                                    }
                                }
                                break;
                            }
                        case 7:
                            {
                                if (fact.Length > 18)
                                {
                                    result1 = visitServ.CreateVisits(new VisitDetail
                                    {
                                        FactoryId = fact[18],
                                        MonitorId = user.Id,
                                        VisitDate = dateTime
                                    });
                                    if (result1 == true && fact.Length > 19)
                                    {
                                        result2 = visitServ.CreateVisits(new VisitDetail
                                        {
                                            FactoryId = fact[19],
                                            MonitorId = user.Id,
                                            VisitDate = dateTime
                                        });
                                        if (result2 == true)
                                        {
                                            result3 = visitServ.CreateVisits(new VisitDetail
                                            {
                                                FactoryId = fact[20],
                                                MonitorId = user.Id,
                                                VisitDate = dateTime
                                            });
                                            if (result3 == true && fact.Length > 20)
                                            {

                                                result4 = visitServ.CreateVisits(new VisitDetail
                                                {
                                                    FactoryId = fact[21],
                                                    MonitorId = user.Id,
                                                    VisitDate = dateTime
                                                });
                                            }
                                        }
                                    }
                                }
                                break;
                            }

                        case 8:
                            {
                                if (fact.Length > 22)
                                {
                                    result1 = visitServ.CreateVisits(new VisitDetail
                                    {
                                        FactoryId = fact[22],
                                        MonitorId = user.Id,
                                        VisitDate = dateTime
                                    });
                                    if (result1 == true && fact.Length > 23)
                                    {
                                        result2 = visitServ.CreateVisits(new VisitDetail
                                        {
                                            FactoryId = fact[23],
                                            MonitorId = user.Id,
                                            VisitDate = dateTime
                                        });
                                        if (result2 == true && fact.Length > 24)
                                        {
                                            result3 = visitServ.CreateVisits(new VisitDetail
                                            {
                                                FactoryId = fact[24],
                                                MonitorId = user.Id,
                                                VisitDate = dateTime
                                            });
                                            if (result3 == true && fact.Length > 25)
                                            {

                                                result4 = visitServ.CreateVisits(new VisitDetail
                                                {
                                                    FactoryId = fact[25],
                                                    MonitorId = user.Id,
                                                    VisitDate = dateTime
                                                });
                                            }
                                        }
                                    }
                                }
                                break;
                            }

                        default:
                            break;
                    }
                }

                countdays += 1;
                dateTime = dateTime.AddDays(1);
            }
        }
    }
}