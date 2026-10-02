import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createDrone, deleteDrone } from '../api/requests';
import { useGlobalNotification } from '@/dashboard/useGlobalNotification';
import { droneKeys } from '../api/queryKeys';

export const useDroneCreateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: createDrone,
        onSuccess: () => {
            showNotification("Drone created", "success");
            queryClient.invalidateQueries({ queryKey: droneKeys.all });
        },
        onError: () => {
            showNotification("Failed to create drone", "error");
        },
    });
};

export const useDroneDeleteHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: (id: number) => deleteDrone(id),
        onSuccess: () => {
            showNotification("Drone deleted", "success");
            queryClient.invalidateQueries({ queryKey: droneKeys.all });
        },
        onError: () => {
            showNotification("Failed to delete drone", "error");
        },
    });
};