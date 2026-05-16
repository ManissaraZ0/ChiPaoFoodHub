using System;
using System.Collections.Generic;
using FoodHubManagerApp.Model;
using RestApiUtil;

namespace FoodHubManagerApp
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

        public static List<ManagerRestaurantListRsp> GetManagedRestaurants(int managerId)
        {
            return RestUtil.Get<List<ManagerRestaurantListRsp>>(MyConfig.BaseUri, $"Manager/v1/restaurants?managerId={managerId}");
        }

        public static PromotionBasicRsp AddPromotion(int restaurantId, int managerId, AddPromotionReq req)
        {
            // ปรับ Type ของ Request เป็น AddPromotionReq และ Response เป็น PromotionBasicRsp
            return RestUtil.PostWithResult<AddPromotionReq, PromotionBasicRsp>(
                MyConfig.BaseUri, $"Manager/v1/restaurants/{restaurantId}/promotions?managerId={managerId}", req);
        }

        public static List<ManagerTicketDetailRsp> GetRestaurantTickets(int restaurantId, int managerId, string status = null)
        {
            // ปรับ Type ของ Response เป็น List<ManagerTicketDetailRsp>
            string endpoint = $"Manager/v1/restaurants/{restaurantId}/tickets?managerId={managerId}";

            // เพิ่ม query parameter 'status' หากมีการระบุมา
            if (!string.IsNullOrWhiteSpace(status))
            {
                endpoint += $"&status={Uri.EscapeDataString(status)}";
            }

            return RestUtil.Get<List<ManagerTicketDetailRsp>>(
                MyConfig.BaseUri, endpoint);
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
