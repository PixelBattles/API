using System.Collections.Generic;
using System.Linq;

namespace PixelBattles.API.Server
{
    public class Result
    {
        public bool Succeeded { get; protected set; }

        public IEnumerable<Error> Errors { get; protected set; }

        protected Result(bool succeeded, params Error[] errors)
        {
            this.Succeeded = succeeded;
            this.Errors = errors.ToList();
        }

        protected Result(bool succeeded, IEnumerable<Error> errors)
        {
            this.Succeeded = succeeded;
            this.Errors = errors.ToList();
        }

        protected Result()
        {
        }

        private static readonly Result success = new Result { Succeeded = true };

        private static IEnumerable<Error> ToEnumerableError(Error error)
        {
            yield return error;
        }

        public static Result Success => success;

        public static Result Failed(params Error[] errors)
        {
            return Failed(errors as IEnumerable<Error>);
        }

        public static Result Failed(Error error)
        {
            return Failed(ToEnumerableError(error));
        }

        public static Result Failed(IEnumerable<Error> errors)
        {
            return new Result
            {
                Succeeded = false,
                Errors = errors?.ToList()
            };
        }

        public override string ToString()
        {
            return Succeeded ?
                   "Succeeded" :
                   string.Format("Failed : {0}", string.Join(",", Errors.Select(x => x.Code).ToList()));
        }
    }
}
