import type { Drone } from './types';
import { type CustomFormField } from '../../components/FormFields';

export const droneDefault = { droneId: 0, name: "", hostName: "" } as Drone

export const fields = [
    { name: 'name', type: "Text", initialValue: droneDefault.name, validator: (x: string) => x != '', required: true, display: "Name" },
    { name: 'hostName', type: "Text", initialValue: droneDefault.hostName, required: false, validator: (x: string) => x != '', display: "Host Name" }
] as CustomFormField[];