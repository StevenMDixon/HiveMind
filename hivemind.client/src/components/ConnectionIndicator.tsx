import { useImportHub } from "@/providers/SignalRProvider";

function ConnectionIndicator() {
    const { connected } = useImportHub();

    return (
        <span>
            {connected ? "Live updates connected" : "Reconnecting..."}
        </span>
    );
}

export default ConnectionIndicator;