using System.Collections.Generic;
using System.Linq;

namespace PixelBattles.Server.Core
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

        public static Result Success => success;

        public static Result Failed(params Error[] errors)
        {
            return new Result
            {
                Succeeded = false,
                Errors = errors?.ToList()
            };
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
                   string.Format("Failed : {1}", string.Join(",", Errors.Select(x => x.Code).ToList()));
        }
    }
}
