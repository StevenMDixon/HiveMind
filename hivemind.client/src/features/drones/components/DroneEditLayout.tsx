import { Container } from "@mui/material"
import type { EditDrone, Station } from '../types';

import CustomForm from '../../../components/CustomForm';
import { type CustomFormField } from '../../../components/FormFields';
import { useDroneUpdateHook } from '../hooks/droneHooks';


interface DroneEditLayoutProps {
    drone: EditDrone;
    stations: Station[];
}

const DroneEditLayout = ({ drone, stations }: DroneEditLayoutProps) => {
    const { mutate: udpateDrone } = useDroneUpdateHook();

    const handleSaveDrone = async (item: EditDrone) => {

        udpateDrone(item);
    }

    const fields = [
        { name: 'name', display: "Name", type: "Text", initialValue: drone.name },
        { name: 'stations', display: "Station", type: "MultiSelect", initialValue: 1, options: stations },
    ] as CustomFormField[];

    return (
        <Container sx={{ mt: 5 }}>
            <CustomForm title="" save={handleSaveDrone} initialValue={drone} fields={fields} />
        </Container>
    )
}

export default DroneEditLayout;