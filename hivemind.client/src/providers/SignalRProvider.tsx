import {
    createContext,
    useContext,
    useEffect,
    useState,
    type ReactNode,
} from "react";

import {
    HubConnectionBuilder,
    HubConnectionState,
    LogLevel,
} from "@microsoft/signalr";

import { useQueryClient } from "@tanstack/react-query";

import { importKeys } from "./importKeys.ts";
import type { ImportStatus } from "./types.ts";
import { libraryKeys } from "@/features/libraries/api/queryKeys.ts";

interface ImportHubContextValue {
    connected: boolean;
}

const ImportHubContext = createContext<ImportHubContextValue | null>(null);

interface Props {
    children: ReactNode;
}

export function ImportHubProvider({ children }: Props) {
    const queryClient = useQueryClient();
    const [connected, setConnected] = useState(false);

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl("/hubs/import")
            .withAutomaticReconnect()
            .configureLogging(LogLevel.Information)
            .build();

        // Register event handlers before starting the connection.
        connection.on("ImportUpdated", (status: ImportStatus) => {
            console.log(status)
            
            queryClient.setQueryData(
                importKeys.status,
                status
            );


            if (status.state.toString() === "Completed") {
              queryClient.invalidateQueries({
                  queryKey: libraryKeys.lists(),
              });
            }
        });

        connection.onreconnecting(() => {
            setConnected(false);
        });

        connection.onreconnected(() => {
            setConnected(true);
        });

        connection.onclose(() => {
            setConnected(false);
        });

        let disposed = false;

        async function startConnection() {
            while (!disposed) {
                try {
                    if (
                        connection.state === HubConnectionState.Disconnected
                    ) {
                        await connection.start();
                    }

                    if (!disposed) {
                        setConnected(true);
                    }

                    break;
                } catch (error) {
                    console.error("SignalR connection failed:", error);

                    if (!disposed) {
                        await new Promise(resolve =>
                            setTimeout(resolve, 5000)
                        );
                    }
                }
            }
        }

        void startConnection();

        return () => {
            disposed = true;
            connection.off("ImportUpdated");

            void connection.stop();
        };
    }, [queryClient]);

    return (
        <ImportHubContext.Provider value={{ connected }}>
            {children}
        </ImportHubContext.Provider>
    );
}

export function useImportHub() {
    const context = useContext(ImportHubContext);

    if (!context) {
        throw new Error(
            "useImportHub must be used within ImportHubProvider"
        );
    }

    return context;
}