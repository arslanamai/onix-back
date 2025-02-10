namespace Onix.SharedKernel;

public static class Constants
{
    //max length
    public const int CODE_MAX_LENGTH = 2000;
    public const int NAME_MAX_LENGTH = 50;
    public const int EMAIL_MAX_LENGTH = 256;
    public const int SUBDOMAIN_MAX_LENGTH = 50;
    public const int SUB_MAX_LENGTH = 50;
    public const int PATH_MAX_LENGTH = 250;
    
    //min length
    public const int NAME_MIN_LENGTH = 2;
    public const int SUBDOMAIN_MIN_LENGTH = 2;
    
    //regax
    public const string SUBDOMAIN_REGEX = "^[a-z]+(-[a-z]+)?$";
    public const string ID_REGEX = "^([0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12})$";

    //max count
    public const int MAX_BLOCK_COUNT = 10;
    public const int MAX_PRODUCT_COUNT = 25;
    public const int MAX_LOCATION_COUNT = 10;
    public const int MAX_PHOTO_COUNT = 5;
    
    //min count
    public const int MIN_COUNT = 0;
}
