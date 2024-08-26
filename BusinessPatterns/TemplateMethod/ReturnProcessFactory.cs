namespace TemplateMethod
{
  public static class ReturnProcessFactory 
  {
    public static ReturnProcessTemplate CreateFrom(ReturnAction returnAction) 
    {
      if (returnAction == ReturnAction.FaultyReturn)
      {
        return new FaultyReturnProcess();
      }
      if (returnAction == ReturnAction.NoQuibblesReturn) 
      {
        return new NoQuibblesReturnProcess();
      }
      throw new ApplicationException(
        $"No process template defined for the action {returnAction}");
    }
  }
}
