public enum KEventName
{
    KToolTestEvent,
    
    MainPuzzleBeginConnection,      // triggered when you click at a left node of main puzzle. param: nodeIndex(int)
    MainPuzzleEndConnection,      // triggered when you click at a right node of main puzzle. param: nodeIndex(int)
    MainPuzzleCancelConnection,      // triggered when you canceled a connection of main puzzle.
    CountDownEnd,      // triggered when time's up.
    
    AllConnectionSucceed,       // triggered when there's no error in main puzzle
}