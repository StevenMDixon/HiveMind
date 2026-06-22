import { queryOptions } from '@tanstack/react-query';
import { getStation, getStations} from '../api/requests'; 

export const stationQueryOptions = (id: string) => {
    return queryOptions({
        queryKey: ['stations', id],
        queryFn: () => getStation(id)
    });
}

export const stationsQueryOptions = () => {
    return queryOptions({
        queryKey: ['stations'],
        queryFn: getStations
    });
}