import { Container } from "@mui/material"
import type { Station } from '../types';

import CustomForm from '@/components/CustomForm';
import { fields } from '../fields';

import { useStationUpdateHook } from '../hooks/stationHooks';

interface StationEditLayoutProps {
    stationData: Station
}

const StationEditLayout = ({ stationData }: StationEditLayoutProps) => {

    const { mutate: updateStation } = useStationUpdateHook();

    const handleSaveStation = async (item: Station) => {
        updateStation(item);
    }

    return (
        <Container sx={{ mt: 5 }}>
            {
                stationData &&
                <CustomForm title={stationData.name} save={handleSaveStation} initialValue={stationData} fields={fields} />
            }
        </Container>
    )
}

export default StationEditLayout;