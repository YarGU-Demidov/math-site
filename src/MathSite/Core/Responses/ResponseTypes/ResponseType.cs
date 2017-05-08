namespace MathSite.Core.Responses.ResponseTypes
{
	public abstract class ResponseType: IResponseType
	{
		protected ResponseType(string typeName)
		{
			TypeName = typeName;
		}

		public string TypeName { get; }
	}
}