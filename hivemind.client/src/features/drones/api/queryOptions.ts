import { queryOptions } from '@tanstack/react-query';
import { getDrones } from './requests';

export const dronesQueryOptions = () => {
    return queryOptions({
        queryKey: ['drones'],
        queryFn: getDrones
    });
}