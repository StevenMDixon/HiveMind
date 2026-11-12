import { queryOptions } from '@tanstack/react-query';
import { getDrones, getDrone, getStations } from './requests';
import { droneKeys } from './queryKeys';

export const dronesQueryOptions = () => {
    return queryOptions({
        queryKey: [...droneKeys.all],
        queryFn: getDrones
    });
}

export const droneQueryOptions = (id: string) => {
    return queryOptions({
        queryKey: [...droneKeys.all, id],
        queryFn: () => getDrone(id)
    });
}

export const stationsQueryOptions = () => {
    return queryOptions({
        queryKey: [...droneKeys.all, 'stations'],
        queryFn: getStations
    });
}