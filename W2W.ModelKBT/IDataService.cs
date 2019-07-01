using KBTech.Lib.Client.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W2W.ModelKBT.Entities;

namespace W2W.ModelKBT
{
    public interface IDataService
    {
        Partner Login(string identity, string password);
        Partner GetPartner(string identity);
        Partner GetPartner(uint id_object);

        IEnumerable<Partner> GetPartners(uint id_object, string searchText);
        IEnumerable<Partner> GetPartners();
        IEnumerable<Partner> GetPartners(uint rootId);
        IEnumerable<Partner> GetSandboxPartners(uint id_object);
        Partner GetPartnerByEmail(string email);
        void UpdatePartnerPassword(uint id_client, string password);
        void UpdatePartnerEmail(uint id_client, string email);
        void UpdatePartnerProfile(
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
            string soclink);
        uint CreatePartner(
            uint inviterId, // допустимо 0 использовать
            string firstName,
            string lastName,
            string middleName,
            string email,
            string login,
            string
            password);
        void UpdateGoogleAuthKey(
            uint id_client,
            string GoogleAuthKey,
            bool? GoogleAuthEnabled);
        void UpdatePartnerAvatar(string path, uint id_client);
        void UpdatePartnerPassport(uint id_client, string passport1, string passport2, string passport3);
        void UpdatePartnerWallets(uint id_client, 
            string walletBitcoin, 
            string walletEthereum,
            string walletLitecoin,
            string walletBitcoinCash,
            string walletRipple,
            string walletUsdt);
        void UpdatePartnerEmailConfirmation(uint id_client, bool? emailConfirmation);
        void SetPartnerMarketPlaceStatus(uint partnerId, bool status);
        void UpdateParnterVerificationStatus(uint partnerId, string status);
        void UpdatePartnerActivity(uint partnerId, bool? isActive);
        void UpdatePartnerInvestSum(bool isLeftShoulder, decimal shoulderSum, decimal binarySum, uint id_place);
        void UpdatePartnerBinarySum(decimal leftSum, decimal rightSum, uint id_place);

        void AddReview(uint id_client, string review);
        IEnumerable<Article> GetNews();
        Article GetArticle(int id);
        IEnumerable<Partner> GetReviews();
        IEnumerable<Charity> GetCharity();
        Charity GetCharity(uint id_object);
        IEnumerable<Promo> GetPromos();
        IEnumerable<Poll> GetPolls();
        IEnumerable<PollVariant> GetPollVariants(uint pollId);
        PollChoice GetPollChoise(uint pollId, uint partnerId);
        void SelectPollVariant(uint pollVariantId, uint userId);


        byte[] ReadFile(string path);
        string WriteFile(string path);
        void DeleteFile(string path);

        

        IEnumerable<InnerTransfer> GetInnerTransfers(
            uint? partnerId,
            string account,
            string article,
            DateTime? begin,
            DateTime? end,
            uint? orderId,
            string filter);
        uint AddPayment(TransferDirection direction, 
            uint companyId,
            uint partnerId, 
            uint orderId, string article,
            DateTime paymentDate, decimal sum, PaymentMethod paymentMethod, RateOfNDS rateOfNds, string desctiption, decimal paymentComission,
            string user);
        uint CreateReplenishRequest(
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
            string user);
        void UpdateWebPaymentRequest(uint id_object, string transactionId, string address);
        WebPaymentRequest GetWebPaymentRequest(uint id_object);
        void UpdateWebPaymentRequest(uint id_object, decimal sum, decimal currencySum);
        uint CreateWithdrawalRequest(
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
            string user);
        WithdrawalRequest GetWithdrawalRequest(uint id_object);
        void SetWithdrawalDetails(uint id_object, DateTime? processedDate, 
            string processedStatus, string wallet, string destTag, string comment, string user);

        #region Marketing
        MarketingPlace GetPlace(uint placeId);
        IEnumerable<MarketingPlace> GetStructure(uint marketingId);
        IEnumerable<MarketingPlace> GetStructure(uint marketingId, uint rootId);
        int GetReferCount(uint marketingId, uint partnerId);
        int GetPlaceCount(uint marketingId, uint partnerId);
        IEnumerable<MarketingPlace> GetPlaces(uint marketingId, uint partnerId);


        IEnumerable<MarketingPlan> GetMarketingPlans();

        IEnumerable<Investment> GetReferInvestments(uint inviterId);
        IEnumerable<Investment> GetInvestments(uint partnerId, string status);
        Investment GetInvestment(uint investmentId);
        IEnumerable<Investment> GetTerminatedInvestments(uint partnerId);
        void SetProlonged(uint investmentId, bool isProlonged);
        void SetInvestmentStatus(uint investmentId, string status, DateTime changeDate);

        IEnumerable<InnerTransfer> GetExpectedPayments(uint partnerId);


        // svc
        uint CreatePlace(
        uint marketingId,
        uint partnerId,
        uint parentId,
        int? pos,
        decimal entrySum,
        bool isActive,
        string alias,
        string name,
        string rank,
        string user);

        uint CreateInnerTransfer(
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
            string user);

        void PayPayment(uint id_object, DateTime date);
        void RemovePayment(uint id_object);

       
        IEnumerable<StructPayment> GetStructSavings(uint placeId);
        decimal GetStructSavingsBalance(uint partnerId);
        

        void SetStructPaymentDetails(uint paymentId, uint placeId, int level);
        InvestProgram GetInvestProgram(decimal sum);
        uint CreateInvestment(uint companyId, uint programId, uint partnerId,
            decimal sum, bool isProlonged, DateTime date, string user);

        IEnumerable<ExpectedPayment> GetExpectedPayments(DateTime toDate);
        IEnumerable<ExpectedPayment> GetExpectedPayments(uint orderId, DateTime toDate);
        void SetPlaceChildCount(uint placeId, int? count);

        #endregion

        IEnumerable<DictItem> GetDictItems(string dictName);

        IEnumerable<KbtFile> GetFiles(uint id_parent);

        decimal GetOwnAndRefsValue(uint id_partner);

        IEnumerable<Notice> GetUnreadedNotices(uint partnerId);
        void SetNoticeAsReaded(uint messageId);
        void AddNotice(uint partnerId,string message);
        IEnumerable<NewInvestProgram> GetInvestPrograms();
        NewInvestProgram GetInvestProgram(uint id);

        NewInvestProgram GetNewInvestProgram(decimal sum);

        void UpdateBlockedPayment(uint id_client, decimal blockedSum, decimal sum);
    }
}
