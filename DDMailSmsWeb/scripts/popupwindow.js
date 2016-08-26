(function (global, undefined) {
    var button = null;

    function openButton_load(sender, args) {
        button = sender;
    }

    function OnClientCommand(sender, eventArgs) {
        logEvent("<strong>OnClientCommand</strong>: Command is " + eventArgs.get_commandName());
    }
    function OnClientResizeEnd(sender, eventArgs) {
        logEvent("<strong>OnClientResizeEnd</strong>: RadWindow is resize ended");
    }

    function OnClientResizeStart(sender, eventArgs) {
        logEvent("<strong>OnClientResizeEnd</strong>: RadWindow is resize started");
    }

    function OnClientDragStart(sender, eventArgs) {
        logEvent("<strong>OnClientDragStart</strong>: RadWindow drag started");
    }

    function OnClientDragEnd(sender, eventArgs) {
        logEvent("<strong>OnClientDragEnd</strong>: RadWindow drag ended");
    }

    function OnClientPageLoad(sender, eventArgs) {
        logEvent("<strong>OnClientPageLoad</strong>: RadWindow completed loading the page");
    }

    function OnClientActivate(sender, eventArgs) {
        //LogEvent("<strong>OnClientActivate</strong>: RadWindow is activated.");
    }

    function OnClientBeforeClose(sender, eventArgs) {
        logEvent("<strong>OnClientBeforeClose</strong>: RadWindow is closing.");
    }

    function OnClientclose(sender, eventArgs) {
        button.set_enabled(true);
        var arg = eventArgs.get_argument();
        if (arg) {
            radalert("A custom argument was passed. Its value is: " + arg);
            logEvent("<strong>OnClientClose</strong>: The RadWindow is closed with an argument. The provided argument is:  " + arg);
        }
        else {
            logEvent("<strong>OnClientClose</strong>: RadWindow is closed");
        }
    }

    function OnClientshow(sender, eventArgs) {
        button.set_enabled(false);
        logEvent("<strong>OnClientShow</strong>: RadWindow is shown.");
    }

    function OnClientBeforeShow(sender, eventArgs) {
        logEvent("<strong>OnClientBeforeShow</strong>: RadWindow is showing.");
    }

    function OnClientAutoSizeEnd(sender, eventArgs) {
        logEvent("<strong>OnClientAutoSizeEnd</strong>: RadWindow is autosized.");
    }

    function OpenWnd() {
        radopen(null, "RadWindow1");
    }

    global.openButton_load = openButton_load;
    global.OnClientCommand = OnClientCommand;
    global.OnClientResizeEnd = OnClientResizeEnd;
    global.OnClientResizeStart = OnClientResizeStart;
    global.OnClientDragStart = OnClientDragStart;
    global.OnClientDragEnd = OnClientDragEnd;
    global.OnClientPageLoad = OnClientPageLoad;
    global.OnClientActivate = OnClientActivate;
    global.OnClientBeforeClose = OnClientBeforeClose;
    global.OnClientclose = OnClientclose;
    global.OnClientshow = OnClientshow;
    global.OnClientBeforeShow = OnClientBeforeShow;
    global.OnClientAutoSizeEnd = OnClientAutoSizeEnd;
    global.OpenWnd = OpenWnd;
})(window);