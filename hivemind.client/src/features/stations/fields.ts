import type { Station } from './types';
import { type CustomFormField } from '../../components/FormFields';

export const stationDefault = { id: 0, name: "", number: 1 } as Station

export const fields = [
    { name: 'name', type: "Text", initialValue: stationDefault.name, validator: (stationName: string) => stationName != '', required: true, display: "Station Name"},
    { name: 'number', type: "Number", initialValue: stationDefault.number, required: false, validator: (stationNumber: string) => Number(stationNumber) > 0, display: "Station Number"}
] as CustomFormField[];