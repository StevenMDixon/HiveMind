import Container from '@mui/material/Container';
import Header from '@/components/Header';
// import CustomTable, { type CellData } from '@/components/CustomTable';

import type { Drone } from '../types';

import { useQuery } from '@tanstack/react-query';
import { dronesQueryOptions } from '../api/queryOptions';

// import Card from '@mui/material/Card';
// import CardHeader from '@mui/material/CardHeader';
// import CardMedia from '@mui/material/CardMedia';
// import CardContent from '@mui/material/CardContent';
// import Typography from '@mui/material/Typography';
// import CardActions from '@mui/material/CardActions';


import CustomDialog from '@/components/Dialog';
import { droneDefault, fields } from '../fields';
import { useDroneCreateHook, useDroneDeleteHook } from '../hooks/droneHooks';


import DroneCard from '../components/DroneCard';


interface DroneCardsProps {
    drones: Drone[] | undefined
}

const DroneCardWrapper = ({ drones }: DroneCardsProps) => {
    const { mutate: deleteDrone } = useDroneDeleteHook();

    return (
        <Container sx={{
            width: '100%',
            display: 'grid',
            gridTemplateColumns: 'repeat(auto-fill, minmax(min(300px, 100%), 1fr))',
            gap: 2,
            marginTop: "1em"
        }} >
            {
                drones && drones.map(x => <DroneCard drone={x} remove={deleteDrone} />)
            }
        
       
        </Container>
    )
}


const DronesPage = () => {
    const { data: drones, isLoading } = useQuery(dronesQueryOptions());


    const { mutate: createDrone } = useDroneCreateHook();
    

    // const columns = [
    //     { key: 'droneId', name: 'ID', align: 'left' },
    //     { key: 'name', name: 'Name', align: 'left' },
    //     { key: 'hostName', name: 'HostName', align: 'left' },
    // ] as CellData<Drone>[];


    // const handleRetry = () => {
    //     refetch();
    // };

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