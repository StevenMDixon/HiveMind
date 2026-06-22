import type { Station } from './types';
import { type CustomFormField } from '../../components/FormFields';

export const stationDefault = { stationId: 0, stationName: "", stationNumber: 1 } as Station

export const fields = [
    { name: 'stationName', type: "Text", initialValue: stationDefault.stationName, validator: (stationName: string) => stationName != '', required: true, display: "Station Name"},
    { name: 'stationNumber', type: "Number", initialValue: stationDefault.stationNumber, required: false, validator: (stationNumber: string) => Number(stationNumber) > 0, display: "Station Number"}
] as CustomFormField[];