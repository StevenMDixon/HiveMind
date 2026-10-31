import Container from '@mui/material/Container';
import Header from '@/components/Header';

import type { Drone } from '../types';
import { useQuery } from '@tanstack/react-query';
import { dronesQueryOptions } from '../api/queryOptions';

import CustomDialog from '@/components/Dialog';
import { droneDefault, fields } from '../fields';
import { useDroneCreateHook, useDroneDeleteHook } from '../hooks/droneHooks';

import DroneCard from '../components/DroneCard';
import { useNavigate } from "react-router-dom";


interface DroneCardsProps {
    drones: Drone[] | undefined
}

const DroneCardWrapper = ({ drones }: DroneCardsProps) => {
    const { mutate: deleteDrone } = useDroneDeleteHook();


    const navigate = useNavigate();

    return (
        <Container sx={{
            width: '100%',
            display: 'grid',
            gridTemplateColumns: 'repeat(auto-fill, minmax(min(300px, 100%), 1fr))',
            gap: 2,
            marginTop: "1em"
        }} >
            {
                drones && drones.map(x => <DroneCard drone={x} remove={deleteDrone} edit={() => navigate("/drones/"+ x.id)} />)
            }
        
       
        </Container>
    )
}


const DronesPage = () => {
    const { data: drones, isLoading } = useQuery(dronesQueryOptions());

    const { mutate: createDrone } = useDroneCreateHook();
    
    console.log(drones)

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Drones">
                <CustomDialog buttonText="Add New Drone" title="Create Drone" save={createDrone} initialValue={droneDefault} fields={fields} />
            </Header>
            {/* <CustomTable data={drones} handleRetry={handleRetry} columns={columns} isLoading={isLoading} /> */}
            {!isLoading && <DroneCardWrapper drones={drones}/>}
        </Container>
    )
}

export default DronesPage;