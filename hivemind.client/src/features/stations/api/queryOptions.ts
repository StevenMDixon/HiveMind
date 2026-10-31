import { queryOptions } from '@tanstack/react-query';
import { getStation, getStations, search} from '../api/requests'; 
import { stationKeys } from './queryKeys';

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


export const stationsQuerySearchOptions = (searchTerm: string, page: number, count: number) => {
    return queryOptions({
        queryKey: [...stationKeys.all, searchTerm, page, count],
        queryFn: () => search(searchTerm, page, count)
    });
}