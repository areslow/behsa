using Microsoft.AspNetCore.Http;

namespace bghBackend.Infra.Utilities
{
    /// <summary>
    /// Static Data
    /// </summary>
    public static class SD
    {
        //roles
        public const string ROLE_ADMIN = "admin";
        public const string ROLE_TEACHER = "teacher";
        public const string ROLE_MEMBER = "member";
        public const string ROLE_SUPPORT = "support"; 

        //post type
        public const string POST_TYPE_POST = "post";
        public const string POST_TYPE_ARTICLE = "article";

        //search type
        public const string SEARCH_INCLUDE = "include";
        public const string SEARCH_MATCH = "match";

        /// <summary>
        /// query result messages
        /// </summary>

        // user registration successfull
        public const string USER_CREATED = "حساب کاربری شما با موفقیت ثبت شد";

        // creation failed message
        public const string CREATION_FAILED = "Failed to create item!";
        // creation successfull message
        public const string CREATION_SUCCESSFULL = "Item has been created Successfully";
        // not found message
        public const string ITEM_NOT_FOUND = "No Item has been found on the server!";
        // no change has been made message
        public const string ITEM_NOT_CHANGED = "Item has not been changed!";
        // update successfull mwssage
        public const string ITEM_UPDATED = "Item has been updated successfully!";
        // deleted successfully
        public const string ITEM_REMOVED = "Item has been removed successfully";
        // retrieved success 
        public const string RETRIVED_SUCCESSFULLY = "Item(s) been retrived successfully";
        // action failed
        public const string ACTION_FAILED = "Action failed"; 

        public const string ACTION_UNAUTHORIZED = "you're not authorized to do this action";

        // anonymous user
        public const string ANONYMOUS_USER = "anonymous";


        // userName exists message
        public const string USERNAME_EXISTS = "Username already exist";

        // username or password incorrocet not exists
        public const string WRONG_LOGIN_INFO = "نام کاربری یا رمز عبور اشتباه است  ";
        // signin successfull msg
        public const string SIGNIN_SUCCESSFULL = "You are signed in successfully";

        // number of days that the jwt token is expired after. for login ...
        // could be a month o more . 
        public const int JWT_TOKEN_EXPIRE_DAYS = 7; // days



        /// council request states
        public const string REQUEST_NEED_FOLLOW_UP = "unseen";
        public const string REQUEST_PENDING = "pending"; // neeed to take action
        public const string REQUEST_ENDED_NO_RESULT = "no result";
        public const string REQUEST_DONE = "ended";
        public const string NO_ACTION = "no action";








        /// storage paths
        /// product image stroage
        public const string ProductImageDirectory = "C:\\Users\\Davood\\Desktop\\React\\Behzad\\behzad\\src\\Assets\\img\\ProductImages";


    }
}
