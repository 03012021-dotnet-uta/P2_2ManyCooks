
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.LogicModels;
using Repository.Models;

namespace Service.Logic
{
    public interface IUserLogic
    {
        List<User> getAllUsers();
        // User GetUserData(string sub);


        /// <summary>
        /// Takes in a new authmodel, and Dictionary from Auth0 <br/>
        /// Saves the changes and returns the new model. <br/>
        /// Outputs New saved model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>True if success</returns>
        AuthModel UpdateUser(AuthModel authModel, Dictionary<string, string> userDictionary);

        /// <summary>
        /// Gets the token which is the bearer token from Angular. <br/>
        /// Then makes a request to the Auth0 API to get the user info. <br/>
        /// Then checks our DB if the user exists or not. <br/>
        /// Then if it does it saves new info from dictionary,
        /// and it outputs an AuthModel from the database. <br/>
        /// If not, it outputs an empty AuthModel
        /// </summary>
        /// <param name="token"></param>
        /// <returns>True if user exists in DB</returns>
        bool CheckIfNewUser(Dictionary<string, string> userDataDictionary, out AuthModel authmodel);

        /// <summary>
        /// Can only use this method if User is already saved in our DB. <br/>
        /// Recieves the dictionary with all user data from Auth0.
        /// Then uses it to Update the information in our DB in case user changed their data. <br/>
        /// </summary>
        /// <param name="userData"></param>
        /// <returns>True if success</returns>
        bool UpdateUserDataDictionary(Dictionary<string, string> userData);

        /// <summary>
        /// Gets the current user from the database
        /// using the sub
        /// </summary>
        /// <param name="sub"></param>
        /// <returns>AuthModel from the db</returns>
        AuthModel GetCurrentUserData(string sub);



        /// <summary>
        /// Takes in authModel containing first and last name. <br/>
        /// Takes in dictionary retrieved from Auth0. <br/>
        /// Saves user in db. <br/>
        /// </summary>
        /// <param name="authModel"></param>
        /// <param name="userDictionary"></param>
        /// <param name="newModel"></param>
        /// <returns>AuthModel of new User</returns>
        bool CreateNewUser(AuthModel authModel, Dictionary<string, string> userDictionary, out AuthModel newModel);
    }
}

