import './App.css';
import {useEffect, useState} from 'react';
import {Container, Stack, Grid, Typography, Box} from '@mui/material';
import SearchForm from './components/SearchForm';
import ResultsTable from './components/ResultsTable';
import DistinctSearchHistoryList from './components/DistinctSearchHistoryList';
import {getWebsiteRanks, WebsiteRankDto, SeoRequestDto, getHighestWebsiteRanksPerDay} from './services/Api';

const defaultSearchKeyword = 'land registry searches';
const defaultWebsiteUrl = 'www.infotrack.co.uk';
    
export default function App() {
    const [searchKeyword, setSearchKeyword] = useState<string>(defaultSearchKeyword);
    const [websiteUrl, setWebsiteUrl] = useState<string>(defaultWebsiteUrl);
    const [results, setResults] = useState<WebsiteRankDto[]>([]);
    const [highestDaily, setHighestDaily] = useState<WebsiteRankDto[]>([]);

    const handleHighestWebsiteRanksPerDay = async () => {
        const results = await getHighestWebsiteRanksPerDay({
            searchKeyword: searchKeyword,
            websiteUrl: websiteUrl,
        });
        setHighestDaily(results);
    };
    
    const handleSearch = async (searchKeyword: string, websiteUrl: string) => {
        const seoRequestDto: SeoRequestDto = { searchKeyword, websiteUrl };
        const response = await getWebsiteRanks(seoRequestDto);
        setResults(response);
        handleHighestWebsiteRanksPerDay();
    };

    const handleHistorySelect = (searchKeyword: string, websiteUrl: string) => {
        setSearchKeyword(searchKeyword);
        setWebsiteUrl(websiteUrl);
    };

    return (
        <>
            {/* Fixed list on the left side */}
            <Box
                sx={{
                    position: 'fixed',
                    left: 0,
                    top: 0,
                    bottom: 0,
                    width: '15%', // Adjust width as needed
                    overflowY: 'auto',
                    bgcolor: 'background.paper',
                    padding: 2,
                    boxShadow: 2, // Optional: adds a shadow for better visibility
                }}
            >
                <Typography variant="h5" gutterBottom>
                    History
                </Typography>
                <DistinctSearchHistoryList onSelect={handleHistorySelect} />
            </Box>

            {/* Main content area */}
            <Container sx={{ marginLeft: '15%' }}> {/* Adjust marginLeft to match the width of the fixed list */}
                <Typography variant="h4" gutterBottom>
                    Google SERP Scraper
                </Typography>
                <Grid container spacing={0}>
                    <Grid item xs={12}>
                        <SearchForm
                            searchKeyword={searchKeyword}
                            websiteUrl={websiteUrl}
                            onSubmit={handleSearch}
                            onSearchKeywordChange={setSearchKeyword}
                            onWebsiteUrlChange={setWebsiteUrl}
                        />
                        <Typography variant="h6" paddingTop={1} gutterBottom align="left">
                            Results
                        </Typography>
                        {results.length > 0 && <ResultsTable results={results} />}
                        <Typography variant="h6" paddingTop={2} gutterBottom align="left">
                            Top daily positions
                        </Typography>
                        {highestDaily.length > 0 && <ResultsTable results={highestDaily} />}
                    </Grid>
                </Grid>
            </Container>
        </>
    );
};
