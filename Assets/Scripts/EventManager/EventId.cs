public enum EventId
{
    TEST_EVENT,                // 
    UPDATE_STATE,              // StateMachineInfo: Updates state machine information

    // ====== Inventory related events ======
    ON_UPDATE_INVENTORY_VIEW,  // InventoryShowType: Upates hide / show / etc., 
    ON_REFRESH_INVENTORY_VIEW, // null: calls inventory refresh
    ON_ITEM_USED,              // String : GameItemId

    // ====== Puzzle Related Events =========
    ON_EXAMPLE_PUZZLE_FINISHED, // null : calls sync button sequence for p2
    ON_WELL_PUZZLE_FINISHED,   // null : calls sync button sequence
    
    // ====== 
    UPDATE_SCREEN_TRACKER,     // GameScreen: Enum to track game screens
    UPDATE_CURSOR,             // CursorType: Enum to track cursors
}
