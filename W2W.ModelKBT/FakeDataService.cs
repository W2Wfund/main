using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using KBTech.Lib.Client.Payment;
using W2W.ModelKBT.Entities;

namespace W2W.ModelKBT
{
    public class FakeDataService : IDataService
    {
        #region Служебные

        static private List<Partner> Partners;
        static private List<Article> Articles;
        static private List<Poll> Polls;
        static private List<PollVariant> PollVariants;
        static private List<PollChoice> PollChoices;
        static private List<InnerTransfer> InnerTransfers;
        static private List<Charity> Charities;
        static private List<Promo> Promos;
        static private uint global_id = 0;
        static private List<MarketingPlan> Plans;
        static private List<MarketingPlace> _Places;
        static private List<Investment> Investments;
        static Random r = new Random();

        static List<Partner> AddReferals(uint id_inviter, int count)
        {
            var revstatuses = new[] { "На проверке", "Опубликован", "Отклонен" };
            var verstatuses = new[] { "Верифицирован", "На проверке", "Не подтвержден" };
            var inviter = Partners.Single(x => x.id_object == id_inviter);
            List<Partner> result = new List<Partner>();
            for (var i = 0; i < count; i++)
            {

                Partner client1 = new Partner();

                client1.CreationDate = DateTime.Now;
                client1.City = "Москва";
                client1.Country = "Россия";
                client1.Email = "";
                client1.LastName = "Фамилия";
                client1.id_parent = id_inviter;
                client1.id_object = ++global_id;
                client1.hash = inviter.hash + client1.id_object.ToString("X5");
                client1.Image = HttpContext.Current.Server.MapPath("/Content/img/reviews-img.png");
                client1.Login = "ref" + client1.id_object;

                client1.MiddleName = "Отчество";
                client1.FirstName = "Имя";
                client1.ObjectName = "Фамилия Имя Отчество " + client1.id_object;
                client1.Password = "123";
                client1.InviterId = id_inviter;
                client1.BalanceInvestments = 100 * i;
                client1.VerificationStatus = verstatuses[r.Next(verstatuses.Length)];

                client1.Review = "Оказавшись в трудной жизненной ситуации, я всё же решила рискнуть !!! Первый раз в жизни! Ииии спасибо судьбе за все,что со мной случилось, за КешБери! Теперь я точно знаю ,что жизнь нужно любить, что нужно работать и рисковать!";
                client1.ReviewDate = DateTime.Now;
                client1.ReviewStatus = revstatuses[r.Next(revstatuses.Length)];

                Partners.Add(client1);

                result.Add(client1);
            }


          


            return result;
        }

        static FakeDataService()
        {
            #region partner

            Partners = new List<Partner>();
            var partner1 = new Partner();

            partner1.id_object = ++global_id;
            partner1.PersonalId = (new Random()).Next(1000, 10000);
            partner1.Login = "admin";
            partner1.Image = HttpContext.Current.Server.MapPath("/Areas/lk/Content/img/profile_ava.png");
            partner1.VerificationStatus = "Не подтвержден";
            partner1.FirstName = "Иванов";
            partner1.LastName = "Иван_";
            partner1.MiddleName = "Иванович";
            partner1.ObjectName = "Иванов Иван Иванович";
            partner1.Gender = "М";
            partner1.Country = "Россия";
            partner1.City = "Москва";
            partner1.BirthDate = DateTime.Now;
            partner1.PhoneNumber = "89039039033";
            partner1.SocNetworkLink = "vk.com/id_123";
            partner1.Password = "123";
            partner1.Email = "asd@asd.ru";
            partner1.IsEmailConfirmed = true;
            partner1.AccountBitcoin = "AccountBitcoin";
            partner1.AccountEthereum = "AccountEthereum";
            partner1.AccountLitecoin = "AccountLitecoin";
            partner1.AccountBitcoinCash = "AccountBitcoinCash";
            partner1.AccountRipple = "AccountRipple";
            partner1.BalanceInner = 5600;
            partner1.BalanceInvestments = 3600;
            partner1.BalancePercents = 2600;
            partner1.BalanceRewards = 1600;
            partner1.CreationDate = DateTime.Now;
            partner1.Passport1 = "";
            partner1.Passport2 = "";
            partner1.Passport3 = "";
            partner1.VerificationStatus = "Верифицирован";
            partner1.hash = partner1.hash + partner1.id_object.ToString("X5");
            partner1.Review = null;
            partner1.ReviewStatus = null;
            partner1.ReviewDate = null;

            Partners.Add(partner1);

            var refs1 = AddReferals(partner1.id_object, 15);
            foreach (var refer in refs1)
            {
                var refs2 = AddReferals(refer.id_object, 3);
                foreach (var refer2 in refs2)
                {
                    AddReferals(refer2.id_object, 4);
                }
            }

            #endregion

            #region news

            Articles = new List<Article>();

            var article1 = new Article();
            article1.id_object = ++global_id;
            article1.Image = HttpContext.Current.Server.MapPath("/Content/img/news-img.jpg");
            article1.ObjectName = "Саудовский фонд и Lucid Motors заключили соглашение об инвестициях";
            article1.Desc =
                "Суверенный фонд Саудовской Аравии (PIF) и производитель электромобилей Lucid Motors Inc. заключили инвестиционное с оглашение на $1 млрд. Об этом сообщается на сайте американской компании.";
            article1.Text = @"
                                <img src='/Content/img/news-open-img.jpg' alt=''>
                        <p>
                            Министерство финансов, разработавшее законопроект о «защите и поощрении капиталовложений» в России, объяснило, каким образом оно рассчитывает привлечь триллионы рублей частных инвестиций в российскую экономику в предстоящие шесть лет. Это нужно для реализации
                            задачи, которую поставил президент Владимир Путин, по увеличению доли инвестиций в основной капитал в российском ВВП до 25% (с нынешних 21% ВВП).
                        </p>
                        <p class='subtitle'>Стабилизационная оговорка</p>
                        <p>
                            Иванов подчеркивает, что принципиальное отличие предложенного механизма от всех предыдущих попыток властей стимулировать корпорации инвестировать — в «стабилизационной оговорке», которая не просто обещает инвесторам неизменность регуляторных и фискальных
                            условий на весь период реализации проекта (это шесть или 12 лет в зависимости от объема вложений), но гарантирует юридическую (судебную) защиту капиталовложений. В соглашениях будет прописано обязательство правительства компенсировать
                            убытки инвесторов в случае ухудшения условий реализации проекта.
                        </p>
                        <p class='subtitle'>Старые новые проекты</p>
                        <p>
                            Иванов подчеркивает, что принципиальное отличие предложенного механизма от всех предыдущих попыток властей стимулировать корпорации инвестировать — в «стабилизационной оговорке», которая не просто обещает инвесторам неизменность регуляторных и фискальных
                            условий на весь период реализации проекта (это шесть или 12 лет в зависимости от объема вложений), но гарантирует юридическую (судебную) защиту капиталовложений. В соглашениях будет прописано обязательство правительства компенсировать
                            убытки инвесторов в случае ухудшения условий реализации проекта.
                        </p>
                        <img src='/Content/img/news-open-img.jpg' alt=''>
                            ";
            article1.CreationDate = DateTime.Now.AddDays(1);
            Articles.Add(article1);


            var article2 = new Article();
            article2.id_object = ++global_id;
            article2.Image = HttpContext.Current.Server.MapPath("/Content/img/news-img.jpg");
            article2.ObjectName = "Саудовский фонд и Lucid Motors заключили соглашение об инвестициях";
            article2.Desc =
                "Суверенный фонд Саудовской Аравии (PIF) и производитель электромобилей Lucid Motors Inc. заключили инвестиционное с оглашение на $1 млрд. Об этом сообщается на сайте американской компании.";
            article2.Text =
                "Суверенный фонд Саудовской Аравии (PIF) и производитель электромобилей Lucid Motors Inc. заключили инвестиционное с оглашение на $1 млрд. Об этом сообщается на сайте американской компании.";
            article2.CreationDate = DateTime.Now;

            Articles.Add(article2);

            var article3 = new Article();
            article3.id_object = ++global_id;
            article3.Image = HttpContext.Current.Server.MapPath("/Content/img/news-img.jpg");
            article3.ObjectName = "Саудовский фонд и Lucid Motors заключили соглашение об инвестициях";
            article3.Desc =
                "Суверенный фонд Саудовской Аравии (PIF) и производитель электромобилей Lucid Motors Inc. заключили инвестиционное с оглашение на $1 млрд. Об этом сообщается на сайте американской компании.";
            article3.Text =
                "Суверенный фонд Саудовской Аравии (PIF) и производитель электромобилей Lucid Motors Inc. заключили инвестиционное с оглашение на $1 млрд. Об этом сообщается на сайте американской компании.";
            article3.CreationDate = DateTime.Now;

            Articles.Add(article3);

            var article4 = new Article();
            article4.id_object = ++global_id;
            article4.Image = HttpContext.Current.Server.MapPath("/Content/img/news-img.jpg");
            article4.ObjectName = "Саудовский фонд и Lucid Motors заключили соглашение об инвестициях";
            article4.Desc =
                "Суверенный фонд Саудовской Аравии (PIF) и производитель электромобилей Lucid Motors Inc. заключили инвестиционное с оглашение на $1 млрд. Об этом сообщается на сайте американской компании.";
            article4.Text =
                "Суверенный фонд Саудовской Аравии (PIF) и производитель электромобилей Lucid Motors Inc. заключили инвестиционное с оглашение на $1 млрд. Об этом сообщается на сайте американской компании.";
            article4.CreationDate = DateTime.Now;

            Articles.Add(article4);

            #endregion

            #region polls

            Polls = new List<Poll>();
            var poll1 = new Poll
            {
                id_object = ++global_id,
                ObjectName = "Инвесторов из каких стран Вы бы предпочли?",
                ActualDate = DateTime.Now.AddDays(10)
            };
            Polls.Add(poll1);

            PollVariants = new List<PollVariant>();
            var pv1 = new PollVariant
            {
                id_object = ++global_id,
                PollId = poll1.id_object,
                ObjectName = "Германия",
                SortPosition = 1
            };
            PollVariants.Add(pv1);

            var pv2 = new PollVariant
            {
                id_object = ++global_id,
                PollId = poll1.id_object,
                ObjectName = "Япония",
                SortPosition = 2
            };
            PollVariants.Add(pv2);


            var pv3 = new PollVariant
            {
                id_object = ++global_id,
                PollId = poll1.id_object,
                ObjectName = "Китай",
                SortPosition = 3
            };
            PollVariants.Add(pv3);

            var pv4 = new PollVariant
            {
                id_object = ++global_id,
                PollId = poll1.id_object,
                ObjectName = "Россия",
                SortPosition = 4
            };
            PollVariants.Add(pv4);


            PollChoices = new List<PollChoice>();
            #endregion

            #region InnerTransfers

            InnerTransfers = new List<InnerTransfer>();
            foreach (var client in Partners)
            {
                string[] accounts = new string[] { "Остаток.ВнутреннийСчет", "Остаток.ИнвестиционныйСчет", "Остаток.Проценты", "Остаток.Вознаграждения" };
                string[] articles = new string[] { "Проценты", "Вознаграждение", "Пополнение счета", "Вывод средств", "Комиссия" };
                foreach (var accountName in accounts)
                {
                    int count = 50;
                    Random r = new Random();
                    var reminder = 0.0m;
                    for (var i = 0; i < count; i++)
                    {
                        var instance = new InnerTransfer();
                        instance.id_object = ++global_id;
                        instance.PaymentDirection = r.Next() % 2 == 0 ? "Приход" : "Расход";
                        instance.PaymentArticle = articles[r.Next(articles.Length)];
                        var sum = r.Next(100, 1200) * 10;
                        instance.PaymentSum = sum;
                        if (instance.PaymentDirection == "Приход")
                        {
                            instance.DebetSum = sum;
                            reminder += sum;
                        }
                        else
                        {
                            instance.CreditSum = sum;
                            reminder -= sum;
                        }
                        instance.AccountBalance = reminder;
                        if (instance.PaymentDirection == "Приход")
                        {
                            instance.Comment = "Пополненине счета";
                        }
                        else
                        {
                            instance.Comment = "Вывод средств";
                        }
                        instance.PaymentDate = DateTime.Now.AddDays(-count).AddDays(i);
                        instance.AccountName = accountName;
                        instance.PartnerId = client.id_object;
                        InnerTransfers.Add(instance);
                    }
                }
            }
            #endregion

            #region Charities

            Charities = new List<Charity>();

            var ch1 = new Charity();
            ch1.id_object = ++global_id;
            ch1.Image = HttpContext.Current.Server.MapPath("/Areas/lk/Content/img/charity-1.png");
            ch1.ObjectName = "Фонд Константина Хабенского";
            ch1.Comment = "Благотворительный Фонд Константина Хабенского был создан в 2008 году. Фонд занимается организацией помощи детям с онкологическими и другими тяжёлыми заболеваниями головного мозга.";
            Charities.Add(ch1);

            var ch2 = new Charity();
            ch2.id_object = ++global_id;
            ch2.Image = HttpContext.Current.Server.MapPath("/Areas/lk/Content/img/charity-2.png");
            ch2.ObjectName = "Фонд Ройзмана";
            ch2.Comment = "Благотворительный Фонд Константина Хабенского был создан в 2008 году. Фонд занимается организацией помощи детям с онкологическими и другими тяжёлыми заболеваниями головного мозга.";
            Charities.Add(ch2);

            var ch3 = new Charity();
            ch3.id_object = ++global_id;
            ch3.Image = HttpContext.Current.Server.MapPath("/Areas/lk/Content/img/charity-3.png");
            ch3.ObjectName = "Фонд Ройзмана";
            ch3.Comment = "Благотворительный Фонд Константина Хабенского был создан в 2008 году. Фонд занимается организацией помощи детям с онкологическими и другими тяжёлыми заболеваниями головного мозга.";
            Charities.Add(ch3);

            #endregion

            #region Promo

            Promos = new List<Promo>();

            var promo1 = new Promo();
            promo1.id_object = ++global_id;
            promo1.Image = HttpContext.Current.Server.MapPath("/Areas/lk/Content/img/charity-1.png");
            promo1.ObjectName = "Рекламная компания фонда Константина Хабенского";
            promo1.DownloadFile = "";
            Promos.Add(promo1);

            var promo2 = new Promo();
            promo2.id_object = ++global_id;
            promo2.Image = HttpContext.Current.Server.MapPath("/Areas/lk/Content/img/charity-2.png");
            promo2.ObjectName = "Рекламная компания фонда Константина Хабенского";
            promo2.DownloadFile = "";
            Promos.Add(promo2);

            var promo3 = new Promo();
            promo3.id_object = ++global_id;
            promo3.Image = HttpContext.Current.Server.MapPath("/Areas/lk/Content/img/charity-3.png");
            promo3.ObjectName = "Рекламная компания фонда Константина Хабенского";
            promo3.DownloadFile = "";
            Promos.Add(promo3);

            #endregion

            #region Plans

            Plans = new List<MarketingPlan>();
            var plan1 = new MarketingPlan
            {
                id_object = ++global_id,
                ObjectName = "Накопительная программа",
                hash = 1.ToString("X5") + global_id.ToString("X5")
            };
            Plans.Add(plan1);


            var plan2 = new MarketingPlan
            {
                id_object = ++global_id,
                ObjectName = "Инвестиционная программа",
                hash = 1.ToString("X5") + global_id.ToString("X5")
            };
            Plans.Add(plan2);


            #endregion

            #region Places
            _Places = new List<MarketingPlace>();
            var rnd = new Random();
            for (var i = 0; i < Plans.Count; i++)
            {
                var root = new MarketingPlace
                {
                    id_object = ++global_id,
                    id_parent = Plans[i].id_object,
                    ObjectName = partner1.ObjectName,
                    Alias = partner1.Login,
                    MarketingId = Plans[i].id_object,
                    Comment = null,
                    CreationDate = DateTime.Now,
                    PartnerId = partner1.id_object,
                    hash = Plans[i].hash + global_id.ToString("X5"),
                    ChildCount = 0,
                    Editor = "admin",
                    EntrySum = 1000,
                    IsActive = true,
                    Rank = null,
                    SortKey = null,
                    SortPosition = null,
                    type = "Маркетинговое место"
                };
                _Places.Add(root);





                var maxLevel = rnd.Next(3, 5);
                var childCount = rnd.Next(1, 3);
                CreatePlace(rnd, root, 0, maxLevel, _Places, new decimal[] { 300m, 1000m, 2000m, 3000m, 4000m, 5000m }, 0);
                CreatePlace(rnd, root, 0, maxLevel, _Places, new decimal[] { 300m, 1000m, 2000m, 3000m, 4000m, 5000m }, 1);
            }


            #endregion

            #region Investments
            Investments = new List<Investment>();
            var statuses = new[] { "Активен", "Завершен", "На расторжении" };
            var monthPercents = new decimal[] { 0.025m, 0.03m, 0.04m, };
            foreach (var partner in Partners)
            {
                var count = rnd.Next(2, 10);
                for (var i = 1; i < count; i++)
                {
                    var startDate = DateTime.Now.AddDays(-1 * rnd.Next(10, 40));
                    var days = (DateTime.Now - startDate).TotalDays;
                    var sum = rnd.Next(1000, 50000);
                    var monthPercent = monthPercents[rnd.Next(monthPercents.Length)];
                    var inv = new Investment
                    {
                        id_object = ++global_id,
                        hash = partner.hash + global_id.ToString("X5"),
                        id_parent = partner.id_object,
                        Editor = "admin",
                        ObjectName = "",
                        CreationDate = DateTime.Now,
                        Comment = null,
                        Status = statuses[rnd.Next(statuses.Length)],
                        type = "ИнвестиционныйДоговор",
                        SortPosition = null,
                        Number = Convert.ToInt32(global_id),
                        PartnerId = partner.id_object,
                        Sum = sum,
                        SortKey = null,
                        EndDate = startDate.AddYears(1),
                        StartDate = startDate,
                        SumIncome = Convert.ToDecimal(days) * monthPercent / 30m * sum,

                    };

                    if (inv.Status == "На расторжении")
                    {
                        var RecalcPercent = 0.01m;
                        inv.RecalcSum = inv.Sum * RecalcPercent * Convert.ToDecimal((DateTime.Now - startDate).TotalDays) / 30;
                        inv.StatusChangedDate = DateTime.Now;
                    }

                    if (inv.Sum >= 10e3m)
                    {
                        inv.ProgramName = "Profi";
                    }
                    else if (inv.Sum >= 5000)
                    {
                        inv.ProgramName = "Optimal";
                    }
                    else
                    {
                        inv.ProgramName = "Base";
                    }

                    var numb = 0;
                    DateTime d = inv.StartDate.Value.Date;
                    for (; d <= inv.EndDate.Value.Date; d = d.AddDays(1))
                    {
                        InnerTransfer it = new InnerTransfer
                        {
                            id_object = ++global_id,
                            id_parent = inv.id_object,
                            hash = inv.hash + global_id.ToString("X5"),
                            CreationDate = d,
                            ObjectName = null,
                            PartnerId = inv.PartnerId,
                            Comment = null,
                            PaymentSum = inv.Sum * monthPercent / 30,
                            DebetSum = inv.Sum * monthPercent / 30,
                            CreditSum = null,
                            OrderId = inv.id_object,
                            SortKey = null,
                            SortPosition = null,
                            Editor = "admin",
                            type = "ОжидаемыйПлатеж",
                            PaymentDirection = "Приход",
                            PaymentArticle = "Проценты",
                            AccountBalance = null,
                            PaymentNumber = ++numb,
                            AccountName = "Остаток.Проценты",
                            PaymentDate = d,
                            PaymentMethod = "Внутренний перевод",
                            PaymentStatus = d > DateTime.Today ? "Не оплачен" : "Оплачен"
                        };
                        InnerTransfers.Add(it);
                    }
                    InnerTransfers.Add(new InnerTransfer
                    {
                        id_object = ++global_id,
                        id_parent = inv.id_object,
                        hash = inv.hash + global_id.ToString("X5"),
                        CreationDate = d,
                        ObjectName = null,
                        PartnerId = inv.PartnerId,
                        Comment = null,
                        PaymentSum = inv.Sum,
                        DebetSum = inv.Sum,
                        CreditSum = null,
                        OrderId = inv.id_object,
                        SortKey = null,
                        SortPosition = null,
                        Editor = "admin",
                        type = "ОжидаемыйПлатеж",
                        PaymentDirection = "Приход",
                        PaymentArticle = "Тело инвестиции",
                        AccountBalance = null,
                        PaymentNumber = ++numb,
                        AccountName = "Остаток.ВнутреннийСчет",
                        PaymentDate = d,
                        PaymentMethod = "Внутренний перевод",
                        PaymentStatus = d > DateTime.Today ? "Не оплачен" : "Оплачен"
                    });


                    Investments.Add(inv);
                }
            }
            #endregion

        

        }

        static void CreatePlace(Random rnd, MarketingPlace parent, int level, int maxLevel, List<MarketingPlace> structure,
            decimal[] allowSums, int pos)
        {
            var levels = rnd.Next(3, 7);
            MarketingPlace place = new MarketingPlace
            {
                id_object = ++global_id,
                id_parent = parent.id_object,
                ObjectName = "Object Name " + structure.Count(x => x.MarketingId == parent.MarketingId),
                Alias = "Alias " + structure.Count(x => x.MarketingId == parent.MarketingId),
                MarketingId = parent.MarketingId,
                Comment = null,
                CreationDate = DateTime.Now,
                PartnerId = 0,
                hash = parent.hash + global_id.ToString("X5"),
                ChildCount = 0,
                Editor = "admin",
                EntrySum = allowSums[rnd.Next(allowSums.Length)],
                IsActive = rnd.Next(2) > 0,
                Rank = null,
                SortKey = null,
                SortPosition = pos,
                type = "Маркетинговое место"
            };
            parent.ChildCount++;

            structure.Add(place);
            if (level < maxLevel)
            {
                var childCount = rnd.Next(1, 3);
                for (var c = 0; c < childCount; c++)
                {
                    CreatePlace(rnd, place, level + 1, maxLevel, structure, allowSums, c);
                }
            }

        }

        T Clone<T>(object instance) where T : new()
        {
            if (instance == null)
                return default(T);

            var typeSource = instance.GetType();
            var typeDest = typeof(T);
            T result = new T();
            foreach (var propSource in typeSource.GetProperties())
            {
                var value = propSource.GetValue(instance);
                var propDest = typeDest.GetProperty(propSource.Name);
                if (propDest != null)
                    propDest.SetValue(result, value);
            }
            return result;
        }

        #endregion
        public void UpdatePartnerActivity(uint partnerId, bool? isActive)
        {
            throw new NotImplementedException();
        }
        public decimal GetStructSavingsBalance(uint partnerId)
        {
            throw new NotImplementedException();
        }

        public void UpdateParnterVerificationStatus(uint partnerId, string status)
        {
            throw new NotImplementedException();
        }

        public void UpdatePartnerInvestSum(bool isLeftShoulder, decimal shoulderSum, decimal binarySum, uint id_place)
        {
            throw new NotImplementedException();
        }

        public void UpdatePartnerBinarySum(decimal leftSum, decimal rightSum, uint id_place)
        {
            throw new NotImplementedException();
        }

        public void  SetPartnerMarketPlaceStatus(uint partnerId, bool status)
        {
            throw new NotImplementedException();
        }
        public void SetNoticeAsReaded(uint messageId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Notice> GetUnreadedNotices(uint partnerId)
        {
            throw new NotImplementedException();
        }
        public void SetWithdrawalDetails(uint id_object, DateTime? processedDate,
            string processedStatus, string wallet, string destTag, string comment, string user)
        {
            throw new NotImplementedException();
        }
        public WithdrawalRequest GetWithdrawalRequest(uint id_object)
        {
            throw new NotImplementedException();
        }
        public uint CreateWithdrawalRequest(
           string accountName,
           uint companyId,
           uint partnerId,
           string article,
           DateTime date,
           decimal sum,
           decimal currencySum,
           string currency,
           string wallet,
           string tag,
           string paymentSystem,
           PaymentMethod paymentMethod,
           string comment,
           string documentType,
           string user)
        {
            throw new NotImplementedException();
        }

        public void UpdateWebPaymentRequest(uint id_object, decimal sum, decimal currencySum)
        {
            throw new NotImplementedException();
        }
        public WebPaymentRequest GetWebPaymentRequest(uint id_object)
        {
            throw new NotImplementedException();
        }
        public void UpdateWebPaymentRequest(uint id_object, string transactionId, string address)
        {
            throw new NotImplementedException();
        }

        public uint CreateReplenishRequest(
            string accountName,
            uint companyId,
            uint partnerId,
            string article,
            DateTime date,
            decimal sum,
            decimal currencySum,
            string currency,
            string paymentSystem,
            PaymentMethod paymentMethod,
            string comment,
            string documentType,
            string user)
        {
            throw new NotImplementedException();
        }

        public decimal GetOwnAndRefsValue(uint id_partner)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KbtFile> GetFiles(uint id_parent)
        {
            throw new NotImplementedException();
        }


        public Charity GetCharity(uint id_object)
        {
            throw new NotImplementedException();
        }

        public void SetWithdrawDetails(uint id_object, string wallet, string currency, string paymentSystem)
        {
            throw new NotImplementedException();
        }

        public void UpdatePartnerEmailConfirmation(uint id_client, bool? emailConfirmation)
        {
            throw new Exception();
        }
        public IEnumerable<Partner> GetPartners(uint rootId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DictItem> GetDictItems(string dictName)
        {
            throw new NotImplementedException();
        }

        public Partner Login(string login, string password)
        {
            return Clone<Partner>(Partners.SingleOrDefault(x => x.Login == login && x.Password == password));
        }

        public Partner GetPartner(string identity)
        {
            return Clone<Partner>(Partners.SingleOrDefault(p => p.Login == identity || p.PersonalId.ToString() == identity));
        }

        public IEnumerable<Partner> GetPartners(uint id_object, string searchText)
        {
            if (!string.IsNullOrWhiteSpace(searchText))
                return (from x in Partners
                       where
                       x.InviterId == id_object &&
                       (x.ObjectName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1 ||
                       x.Login.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1)
                       select Clone<Partner>(x)).ToList();

            return Partners.Where(x => x.InviterId == id_object);
        }

        public IEnumerable<Partner> GetSandboxPartners(uint id_object)
        {
            throw new NotImplementedException();
        }

        public Partner GetPartnerByEmail(string email)
        {
            return Clone<Partner>(Partners.SingleOrDefault(p => email.Equals(p.Email, StringComparison.OrdinalIgnoreCase)));
        }

       //public IEnumerable<Partner> GetPartners(uint id_inviter)
       // {
       //     var root = Partners.SingleOrDefault(c => c.id_object == id_inviter);
       //     if (root != null)
       //     {
       //         return (from u in Partners
       //                 where u.hash.IndexOf(root.hash) == 0
       //                 select Clone<Partner>(u)).ToList();
       //     }
       //     return null;
       // }
        public void UpdatePartnerProfile(
            uint id_client,
            string login,
            string lastname,
            string firstname,
            string middlename,
            string gender,
            string country,
            string city,
            DateTime? birthdate,
            string phonenumber,
            string soclink)
        {
            var user = Partners.SingleOrDefault(x => x.id_object == id_client);

            user.Login = login;
            user.LastName = lastname;
            user.FirstName = firstname;
            user.MiddleName = middlename;
            user.Gender = gender;
            user.Country = country;
            user.City = city;
            user.BirthDate = birthdate;
            user.PhoneNumber = phonenumber;
            user.SocNetworkLink = soclink;
        }

        public void UpdateGoogleAuthKey(
                   uint id_client,
                   string GoogleAuthKey,
                   bool? GoogleAuthEnabled)
        {
            var user = Partners.SingleOrDefault(x => x.id_object == id_client);

            user.GoogleAuthKey = GoogleAuthKey;
            user.GoogleAuthEnabled = GoogleAuthEnabled;
        }

        public void AddReview(uint id_client, string review)
        {
            var user = Partners.SingleOrDefault(x => x.id_object == id_client);

            user.Review = review;
            user.ReviewDate = DateTime.Now;
            user.ReviewStatus = "На проверке";
        }

        public IEnumerable<Article> GetNews()
        {
            return Articles.ToList();
        }

        public Article GetArticle(int id)
        {
            return Clone<Article>(Articles.SingleOrDefault(x => x.id_object == id));
        }

        public IEnumerable<Partner> GetReviews()
        {
            return (from x in Partners
                   where x.ReviewStatus == "Опубликован"
                   select Clone<Partner>(x)).ToList();
        }

        public IEnumerable<Promo> GetPromos()
        {
            return Promos.ToList();
        }

        public IEnumerable<Charity> GetCharity()
        {
            return (from x in Charities
                    select Clone<Charity>(x)).ToList();
        }



        public IEnumerable<Poll> GetPolls()
        {
            return (from x in Polls
                   where x.ActualDate == null || x.ActualDate.Value > DateTime.Now
                   select Clone<Poll>(x)).ToList();
        }
        public IEnumerable<PollVariant> GetPollVariants(uint pollId)
        {
            return (from x in PollVariants
                   where x.PollId == pollId
                   orderby x.SortPosition
                   select Clone<PollVariant>(x)).ToList();
        }
        public PollChoice GetPollChoise(uint pollId, uint partnerId)
        {
            return PollChoices.SingleOrDefault(x => x.PollId == pollId && x.PartnerId == partnerId);
        }


        public uint AddWithdraw(DateTime date, string desc, decimal sum, uint refToPartner, string article, string account)
        {
            var partner = Partners.SingleOrDefault(c => c.id_object == refToPartner);
            if (partner != null)
            {
                var instance = new InnerTransfer();
                instance.id_object = ++global_id;
                instance.PaymentDirection = desc;
                instance.PaymentArticle = article;
                instance.PaymentSum = sum;
                if (instance.PaymentDirection == "Приход")
                {
                    instance.DebetSum = sum;
                    instance.AccountBalance = (partner.BalanceInner ?? 0) + sum;
                }
                else
                {
                    instance.CreditSum = sum;
                    instance.AccountBalance = (partner.BalanceInner ?? 0) - sum;
                }
                if (instance.PaymentDirection == "Приход")
                    instance.Comment = "Пополненине счета";
                else
                    instance.Comment = "Вывод средств";
                instance.PaymentDate = date;
                instance.AccountName = account;
                instance.PartnerId = partner.id_object;
                InnerTransfers.Add(instance);

                return instance.id_object;
            }
            return 0;
        }

        #region file
        public byte[] ReadFile(string path)
        {
            return System.IO.File.ReadAllBytes(path);
        }


        public string WriteFile(string path)
        {
            var newpath = Path.Combine(Path.GetDirectoryName(path), "1" + Path.GetFileName(path));
            File.Copy(path, newpath);
            return newpath;
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }
        #endregion

        public void UpdatePartnerAvatar(string path, uint id_client)
        {
            var user = Partners.Single(x => x.id_object == id_client);
            user.Image = path;
        }

        public void UpdatePartnerPassport(uint id_client, string passport1, string passport2, string passport3)
        {
            var user = Partners.Single(x => x.id_object == id_client);
            user.Passport1 = passport1;
            user.Passport2 = passport2;
            user.Passport3 = passport3;
        }

        public void UpdatePartnerPassword(uint id_client, string password)
        {
            var user = Partners.SingleOrDefault(x => x.id_object == id_client);
            user.Password = password;
        }

        public void UpdatePartnerEmail(uint id_client, string email)
        {
            var user = Partners.SingleOrDefault(x => x.id_object == id_client);

            user.Email = email;
        }

        public void UpdatePartnerWallets(uint id_client,
            string walletBitcoin,
            string walletEthereum,
            string walletLitecoin,
            string walletBitcoinCash,
            string walletRipple)
        {
            var user = Partners.SingleOrDefault(x => x.id_object == id_client);
            user.AccountBitcoin = walletBitcoin;
            user.AccountEthereum = walletEthereum;
            user.AccountLitecoin = walletLitecoin;
            user.AccountBitcoinCash = walletBitcoinCash;
            user.AccountRipple = walletRipple;
        }

        public uint CreatePartner(
            uint inviterId,
            string firstName,
            string lastName,
            string middleName,
            string email,
            string login,
            string password)
        {
            var inviter = Partners.SingleOrDefault(x => x.id_object == inviterId);
            Partner instance = new Partner();

            instance.id_object = ++global_id;
            if (inviter != null)
            {
                instance.hash = inviter.hash + instance.id_object.ToString("X5");
                instance.id_parent = inviterId;
            }
            else
                instance.hash = instance.id_object.ToString("X5");
            instance.type = "ФизическоеЛицо";
            instance.CreationDate = DateTime.Now;

            instance.FirstName = firstName;
            instance.LastName = lastName;
            instance.MiddleName = middleName;
            instance.ObjectName = $"{lastName} {firstName} {middleName}";
            instance.Email = email;
            instance.Login = login;
            instance.Password = password;
            instance.PersonalId = 1000 + Convert.ToInt32(global_id);
            Partners.Add(instance);

            return instance.id_object;
        }

        public void SelectPollVariant(uint pollVariantId, uint userId)
        {
            var partner = Partners.SingleOrDefault(x => x.id_object == userId);
            var variant = PollVariants.Single(x => x.id_object == pollVariantId);

            PollChoice choise = new PollChoice
            {
                id_object = ++global_id,
                CreationDate = DateTime.Now,
                PartnerId = partner.id_object,
                PollId = variant.PollId,
                PollVariantId = pollVariantId,
            };
            PollChoices.Add(choise);
            variant.Count++;
        }

        public IEnumerable<string> GetPlaceHashCodes(uint marketingId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<string> GetPlaceHashCodes(uint marketingId, uint partnerId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<InnerTransfer> GetPhilanthropists()
        {
            var max = Partners.Count;
            for (var i=0; i< 6; i++)
            {
                if (i < max)
                {
                    yield return new InnerTransfer
                    {
                        PaymentDate = DateTime.Now.AddDays(-1 * i),
                        PartnerObjectName = Partners[i].ObjectName
                    };
                }
            }
        }

        public uint CreateMarketingPlace(
            uint marketingId,
            uint partnerId,
            uint rootId,
            uint parentId,
            int deep,
            int pos,
            decimal entrySum,
            bool isActive,
            string alias,
            string name,
            string rank)
        {
            throw new NotImplementedException();
        }

        public MarketingPlace GetMarketingPlace(uint id_object)
        {
            throw new NotImplementedException();
        }
        public MarketingPlace GetMarketingPlace(string placeHashCode)
        {
            throw new NotImplementedException();
        }

        public Partner GetPartner(uint id_object)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InnerTransfer> GetInnerTransfers(
            uint? partnerId,
            string account,
            string article,
            DateTime? begin,
            DateTime? end,
            uint? orderId,
            string filter)
        {
            return (from x in InnerTransfers
                    where 
                    (partnerId == null? true : x.PartnerId == partnerId) &&
                    (begin == null ? true : x.PaymentDate >= begin) &&
                    (end == null ? true : x.PaymentDate <= end) &&
                    (string.IsNullOrEmpty(account) ? true : x.AccountName == account) &&
                    (string.IsNullOrEmpty(article) ? true : x.PaymentArticle == article) &&
                    (orderId == null ? true : x.OrderId == orderId) &&
                    (string.IsNullOrWhiteSpace(filter) ? true : x.ObjectName.Equals(filter.Trim(), StringComparison.OrdinalIgnoreCase))
                    select Clone<InnerTransfer>(x)).ToList();

        }

        public uint CreateInnerTransfer(
            string accountName,
            TransferDirection direction,
            uint partnerId,
            uint documentId,
            string article,
            DateTime date,
            decimal sum,
            PaymentMethod paymentMethod,
            string comment,
            string documentType,
            bool IsPayed,
            string user)
        {
            throw new NotImplementedException();
        }

        public void PayPayment(uint id_object)
        {
            throw new NotImplementedException();
        }
        public void CancelPayment(uint id_object)
        {
            throw new NotImplementedException();
        }

        public int GetReferCount(uint partnerId)
        {
            throw new NotImplementedException();
        }

        // Накопления
        public IEnumerable<StructPayment> GetStructSavings(string placeHashCode)
        {
            throw new NotImplementedException();
        }


        #region Marketing

        public MarketingPlace GetPlace(uint placeId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<MarketingPlace> GetStructure(uint marketingId)
        {
            throw new NotImplementedException();
        }
        public int GetReferCount(uint marketingId, uint partnerId)
        {
            throw new NotImplementedException();
        }
        public int GetPlaceCount(uint marketingId, uint partnerId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<MarketingPlace> GetPlaces(uint marketingId, uint partnerId)
        {
            return from p in _Places
                   where p.MarketingId == marketingId && p.PartnerId == partnerId
                   select Clone<MarketingPlace>(p);
        }

        public IEnumerable<MarketingPlan> GetMarketingPlans()
        {
            return from p in Plans
                   select Clone<MarketingPlan>(p);
        }

        public IEnumerable<MarketingPlace> GetStructure(uint marketingId, uint placeId)
        {
            var root = _Places.SingleOrDefault(x => x.id_object == placeId);
            if (root != null)
            {
                return from p in _Places
                       where p.MarketingId == marketingId && p.hash.StartsWith(root.hash)
                       select Clone<MarketingPlace>(p);
            }
            return null;
        }
        #endregion

        public IEnumerable<Partner> GetPartners()
        {
            return (from x in Partners
                   select Clone<Partner>(x)).ToList();
        }

        public IEnumerable<Investment> GetReferInvestments(uint inviterId)
        {
            var partner = Partners.Single(x => x.id_object == inviterId);
            return (from x in Investments
                   where
                   x.hash.StartsWith(partner.hash) && x.PartnerId != inviterId &&
                   x.hash.Length == partner.hash.Length + 5 * 2
                   select Clone<Investment>(x)).ToList();

        }

        public IEnumerable<Investment> GetInvestments(uint partnerId, string status)
        {
            return (from x in Investments
                   where
                   x.PartnerId == partnerId &&
                   (string.IsNullOrWhiteSpace(status) ? true : x.Status == status)
                   select Clone<Investment>(x)).ToList();
        }

        public IEnumerable<Investment> GetTerminatedInvestments(uint partnerId)
        {
            return (from x in Investments
                   where
                   x.PartnerId == partnerId &&
                   x.Status == "На расторжении"
                   select Clone<Investment>(x)).ToList();
        }

        public IEnumerable<InnerTransfer> GetExpectedPayments(uint partnerId)
        {
            return (from x in InnerTransfers
                   where x.type == "ОжидаемыйПлатеж" && x.PartnerId == partnerId
                   select Clone<InnerTransfer>(x)).ToList();
        }

        public Investment GetInvestment(uint investmentId)
        {
            return Clone<Investment>(Investments.SingleOrDefault(x => x.id_object == investmentId));
        }
        public void SetProlonged(uint investmentId, bool isProlonged)
        {
            var investmnent = Investments.SingleOrDefault(x => x.id_object == investmentId);
            if (investmnent != null)
                investmnent.IsProlonged = isProlonged;
        }
        public void SetInvestmentStatus(uint investmentId, string status, DateTime changeDate)
        {
            var investmnent = Investments.SingleOrDefault(x => x.id_object == investmentId);
            if (investmnent != null)
            {
                investmnent.Status = status;
                investmnent.StatusChangedDate = changeDate;

            }
        }
        public uint AddPayment(TransferDirection direction, 
            uint companyId,
            uint partnerId, uint orderId, string article,
            DateTime paymentDate, decimal sum, PaymentMethod paymentMethod, RateOfNDS rateOfNds, string desctiption, decimal paymentComission,
            string user)
        {
            throw new NotImplementedException();
        }
        public void UpdateDigitalPayment(
            uint id_object,
            string blockChain_transactionId,
            string paymentSystem,
            string transactionId)
        {
            throw new NotImplementedException();
        }

        public void AddNotice(uint partnerId, string message)
        {
            throw new NotImplementedException();
        }
        #region for svc
        public uint CreatePlace(
            uint marketingId,
            uint partnerId,
            uint parentId,
            int? pos,
            decimal entrySum,
            bool isActive,
            string alias,
            string name,
            string rank,
            string user)
        {
            throw new NotImplementedException();
        }

        public uint CreateInnerTransfer(
            string accountName,
            TransferDirection direction,
            uint companyId,
            uint partnerId,
            uint documentId,
            string article,
            DateTime date,
            decimal sum,
            PaymentMethod paymentMethod,
            string comment,
            string documentType,
            string user)
        {
            throw new NotImplementedException();
        }

        public void PayPayment(uint id_object, DateTime date)
        {
            throw new NotImplementedException();
        }
        public void RemovePayment(uint id_object)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<StructPayment> GetStructSavings(uint placeId)
        {
            throw new NotImplementedException();
        }


        public void SetStructPaymentDetails(uint paymentId, uint placeId, int level)
        {
            throw new NotImplementedException();
        }
        public InvestProgram GetInvestProgram(decimal sum)
        {
            throw new NotImplementedException();
        }
        public uint CreateInvestment(uint companyId, uint programId, uint partnerId,
            decimal sum, bool isProlonged, DateTime date, string user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExpectedPayment> GetExpectedPayments(DateTime toDate)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ExpectedPayment> GetExpectedPayments(uint orderId, DateTime toDate)
        {
            throw new NotImplementedException();
        }
        public void SetPlaceChildCount(uint placeId, int? count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewInvestProgram> GetInvestPrograms()
        {
            throw new NotImplementedException();
        }

        public NewInvestProgram GetInvestProgram(uint id)
        {
            throw new NotImplementedException();
        }

        public NewInvestProgram GetNewInvestProgram(decimal sum)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
