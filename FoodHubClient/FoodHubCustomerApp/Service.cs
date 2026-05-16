using System;
using System.Collections.Generic;
using FoodHubCustomerApp.Model;
using RestApiUtil;

namespace FoodHubCustomerApp
{
    public static class Service
    {
        // ==========================================
        // ฝั่ง Admin (เรียกใช้ AdminController)
        // ==========================================
        public static List<UserRsp> GetAllUsers()
        {
            return RestUtil.Get<List<UserRsp>>(MyConfig.BaseUri, "admin/v1/users");
        }

        // ==========================================
        // ฝั่ง Customer (เรียกใช้ CustomerController)
        // ==========================================

        public static List<Promotion> BrowsePromotions(int? restaurantId = null)
        {
            string query = restaurantId.HasValue ? $"?restaurantId={restaurantId.Value}" : "";
            return RestUtil.Get<List<Promotion>>(MyConfig.BaseUri, $"Customer/v1/promotions{query}");
        }

        public static PromotionTicket BuyPromotionTicket(int promotionId, int userId)
        {
            // เนื่องจากไม่มี Body ที่ต้องส่งไป ใช้ PostWithResult แบบส่ง null ไปแทน
            return RestUtil.PostWithResult<object, PromotionTicket>(
                MyConfig.BaseUri, $"Customer/v1/promotions/{promotionId}/tickets?userId={userId}", null);
        }

        public static List<Review> GetReviews(int restaurantId)
        {
            return RestUtil.Get<List<Review>>(MyConfig.BaseUri, $"Customer/v1/restaurants/{restaurantId}/reviews");
        }

        public static Review SubmitReview(int restaurantId, SubmitReviewReq req)
        {
            return RestUtil.PostWithResult<SubmitReviewReq, Review>(
                MyConfig.BaseUri, $"Customer/v1/restaurants/{restaurantId}/reviews", req);
        }

        public static List<RestaurantRecommendationRsp> GetRecommendedRestaurants()
        {
            return RestUtil.Get<List<RestaurantRecommendationRsp>>(MyConfig.BaseUri, "Customer/v1/restaurants/recommendations");
        }

        public static CustomerProfileRsp GetCustomerProfile(int userId)
        {
            return RestUtil.Get<CustomerProfileRsp>(MyConfig.BaseUri, $"Customer/v1/profile?userId={userId}");
        }

        public static RestaurantRecommendationRsp GetRestaurantDetail(int restaurantId)
        {
            return RestUtil.Get<RestaurantRecommendationRsp>(MyConfig.BaseUri, $"Customer/v1/restaurants/{restaurantId}/details");
        }

        public static List<RestaurantRecommendationRsp> GetRestaurantBySearchText(string searchText)
        {
            return RestUtil.Get<List<RestaurantRecommendationRsp>>(MyConfig.BaseUri, $"Customer/v1/restaurants/search?searchText={Uri.EscapeDataString(searchText)}");
        }

        // ==========================================
        // ฝั่ง Manager (เรียกใช้ ManagerController)
        // ==========================================

        public static Promotion AddPromotion(int restaurantId, int managerId, Promotion newPromotion)
        {
            return RestUtil.PostWithResult<Promotion, Promotion>(
                MyConfig.BaseUri, $"Manager/v1/restaurants/{restaurantId}/promotions?managerId={managerId}", newPromotion);
        }

        public static List<PromotionTicket> GetRestaurantTickets(int restaurantId, int managerId)
        {
            return RestUtil.Get<List<PromotionTicket>>(
                MyConfig.BaseUri, $"Manager/v1/restaurants/{restaurantId}/tickets?managerId={managerId}");
        }

        public static PromotionTicket ValidateTicket(int ticketId, int managerId)
        {
            // ไม่มี Body ที่ต้องส่ง ใช้ null
            return RestUtil.PostWithResult<object, PromotionTicket>(
                MyConfig.BaseUri, $"Manager/v1/tickets/{ticketId}:validate?managerId={managerId}", null);
        }

        public static List<ManagerPromotionSummaryRsp> GetPromotionSummaries(int restaurantId)
        {
            return RestUtil.Get<List<ManagerPromotionSummaryRsp>>(
                MyConfig.BaseUri, $"Manager/v1/restaurants/{restaurantId}/promotions/summary");
        }

        public static List<ManagerTicketDetailRsp> GetTicketDetails(int restaurantId)
        {
            return RestUtil.Get<List<ManagerTicketDetailRsp>>(
                MyConfig.BaseUri, $"Manager/v1/restaurants/{restaurantId}/tickets-details");
        }

        public static List<ManagerReviewDetailRsp> GetReviewDetails(int restaurantId)
        {
            return RestUtil.Get<List<ManagerReviewDetailRsp>>(
                MyConfig.BaseUri, $"Manager/v1/restaurants/{restaurantId}/reviews-details");
        }
    }
}
