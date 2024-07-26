import axios from 'axios';

const apiClient = axios.create({
  baseURL: '/api/website-ranks',
  headers: {
    'Content-Type': 'application/json'
  },
});

export interface SeoRequestDto {
  searchKeyword: string;
  websiteUrl: string;
}

export interface WebsiteRankDto {
  searchKeyword: string;
  websiteUrl: string;
  resultUrl: string;
  description: string;
  position: number;
  date: string;
}

export const getWebsiteRanks = async (seoRequestDto: SeoRequestDto) => {
  const response = await apiClient.get<WebsiteRankDto[]>('', {
    params: seoRequestDto,
  });
  return response.data;
};

export const getHighestWebsiteRanksPerDay = async (seoRequestDto: SeoRequestDto) => {
  const response = await apiClient.get<WebsiteRankDto[]>('/highest-per-day', {
    params: seoRequestDto,
  });
  return response.data;
};

export const getSearchHistory = async () => {
  const response = await apiClient.get<SeoRequestDto[]>('/unique-search-history');
  return response.data;
};