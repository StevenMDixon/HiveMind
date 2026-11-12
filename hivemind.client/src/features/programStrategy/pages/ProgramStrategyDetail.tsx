import { useParams } from "react-router-dom";
import { programStrategyDetailQueryOptions } from "../api/queryOptions";
import { useQuery } from '@tanstack/react-query';

import Container from '@mui/material/Container';
// import { useNavigate } from "react-router-dom";

import Header from '@/components/Header';


// import EditingGrid, { type EditingGridColumns } from '@/components/EditingGrid';
import ProgramStrategyLayout from "../components/ProgramStrategyLayout";

const ProgramStrategyDetail = () => {
    const { id } = useParams();

    const { data: programStrategy } = useQuery(programStrategyDetailQueryOptions(Number(id ?? '0')));

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title={`Program Strategy: ${id}`}>
            </Header>
            <Container sx={{ mt: 5, width: '100%' }} disableGutters>
                {programStrategy && <ProgramStrategyLayout strategy={programStrategy} />}
            </Container>
        </Container>
    )
}

export default ProgramStrategyDetail;