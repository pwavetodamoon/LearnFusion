using deVoid.Utils;
using UnityEngine;
public class SendMessage : ASignal<string>
{ }
public class RevicedMessage : ASignal<string>
{ }
public class ShowUIChat : ASignal
{ }
public class ShowMovementSignal : ASignal
{ }
public class ShowUIMainMenu : ASignal
{ }
public class HideUIMainMenu : ASignal
{ }
public class ShowUISessionListPanel : ASignal
{ }
public class HideUISessionListPanel : ASignal
{ }
public class ShowUICreateNewSession : ASignal
{ }
public class HideUICreateNewSession : ASignal
{ }

public class OpenChatBox : ASignal
{ }
public class SendPlayerMessage : ASignal
{ }

