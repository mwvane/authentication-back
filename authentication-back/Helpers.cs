namespace authentication_back
{
    public static class Helpers
    {
        public static int GetMaxId(List<User> users)
        {
            int maxId = 0;
            foreach (var user in users)
            {
                if(maxId < user.Id)
                {
                    maxId = user.Id;
                }
            }
            return maxId;
        }

        public static bool isUsernameAlreadyExist(string username, List<User> users)
        {
            foreach (var user in users)
            {
                if(user.Email == username)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
