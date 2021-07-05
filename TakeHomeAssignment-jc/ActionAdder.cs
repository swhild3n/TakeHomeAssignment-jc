using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace TakeHomeAssignment_jc
{
    class ActionAdder
    {
        #region Properties
        static ReaderWriterLockSlim actionLock = new ReaderWriterLockSlim();

        private readonly string errorEmptyAction = "Action is not allowed to be empty";

        private readonly string errorInvalidAction = "Action given is invalid, check your JSON format.";

        private readonly string success = "";

        public List<ActionModel> Actions = new List<ActionModel>();
        #endregion

        /// <summary>
        /// Method to add actions into the program
        /// </summary>
        /// <param name="actionItem">the action being added into the program</param>
        /// <returns>Errors, otherwise an empty string if successful</returns>
        public string addAction(string actionItem)
        {
            if (string.IsNullOrEmpty(actionItem))
                return errorEmptyAction;

            var action = validateAction(actionItem);

            //Action will be null if any errors occur when parsing the JSON
            if (action == null)
                return errorInvalidAction + "\n" + actionItem;

            //Prevents concurrent calls from accessing
            //the Actions list at the same time
            actionLock.EnterWriteLock();
            {
                try 
                {
                    Actions.Add(action);
                }
                finally
                {
                    actionLock.ExitWriteLock();
                }
            }

            return success;
        }

        /// <summary>
        /// Method to get the average time for each action
        /// that has been added to the program
        /// </summary>
        /// <returns>The statistics in a formatted JSON string</returns>
        public string getStats()
        {
            var outputString = "";
            if (Actions == null || Actions.FirstOrDefault() == null)
                return outputString;

            //Makes concurrent reads from the Actions list safe
            actionLock.EnterReadLock();
            {
                try
                {
                    var stats = Actions.GroupBy(x => x.Action)
                    .OrderBy(x => x.Key)
                    .Select(x => new StatsModel()
                    {
                        Action = x.Key,
                        Avg = x.Average(y => y.Time)
                    });
                    outputString = JsonConvert.SerializeObject(stats);
                }
                finally
                {
                    actionLock.ExitReadLock();
                }
                
            }
            return outputString;
        }

        /// <summary>
        /// Method to validate that the string passed in as 
        /// an action matches the correct format for the action
        /// </summary>
        /// <param name="actionString"></param>
        /// <returns></returns>
        private ActionModel validateAction(string actionString)
        {
            ActionModel action = null;
            ActionModel jsonObject = null;

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };

            //Attempts to deserialize the string into a json object using the action model
            try
            {
                jsonObject = JsonConvert.DeserializeObject<ActionModel>(actionString, settings);

                //Ensures that the 'action' property contains an action
                if (string.IsNullOrEmpty(jsonObject.Action))
                    return action;

                //Assuming that time should not be negative
                if (jsonObject.Time < 0)
                    return action;

                action = jsonObject;
                return action;
            }
            catch
            {
                //Unexpected error, return a null object to be handled by call
                return action;
            }
        }

        public class ActionModel
        {
            [JsonProperty("action", Required = Required.Always)]
            public string Action { get; set; }
            [JsonProperty("time", Required = Required.Always)]
            public int Time { get; set; }
        }

        public class StatsModel
        {
            [JsonProperty("action")]
            public string Action { get; set; }
            [JsonProperty("avg")]
            public double Avg { get; set; }
        }
    }
}
