import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createStation, deleteStation, updateStation } from '../api/requests';
import { useGlobalNotification } from '@/dashboard/useGlobalNotification';
import { stationKeys } from '../api/queryKeys';

export const useStationCreateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: createStation,
        onSuccess: () => {
            showNotification("Station created", "success");
            queryClient.invalidateQueries({ queryKey: stationKeys.all });
        },
        onError: () => {
            showNotification("Failed to create station", "error");
        },
    });
};

export const useStationDeleteHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: (id: number) => deleteStation(id),
        onSuccess: () => {
            showNotification("Station deleted", "success");
            queryClient.invalidateQueries({ queryKey: stationKeys.all });
        },
        onError: () => {
            showNotification("Failed to delete station", "error");
        },
    });
};

export const useStationUpdateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: updateStation,
        onSuccess: () => {
            showNotification("Station updated", "success");
            queryClient.invalidateQueries({ queryKey: stationKeys.all });
        },
        onError: () => {
            showNotification("Failed to update station", "error");
        },
    });
};