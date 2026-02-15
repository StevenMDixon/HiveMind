import { createContext, useContext } from 'react';

export const NotificationContext = createContext<{ showNotification: (message: string, severity?: 'error' | 'warning' | 'info' | 'success') => void } | undefined>(undefined);

export const useGlobalNotification = () => {
    const context = useContext(NotificationContext);
    if (!context) {
        throw new Error('useGlobalNotification must be used within a NotificationProvider');
    }
    return context;
};
