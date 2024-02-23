using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand;
public class CreateBrandCommand : IRequest<CreatedBrandDto>
{   
    public string Name { get; set; } = string.Empty;

    public class CreatedBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly BrandBussinesRules _brandBussinesRules;

        public CreatedBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper , BrandBussinesRules brandBussinesRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBussinesRules = brandBussinesRules;   
        }
        
        public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            await _brandBussinesRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);    

            Brand mappedBrand = _mapper.Map<Brand>(request);
            Brand createdBrand = await _brandRepository.AddAsync(mappedBrand);
            CreatedBrandDto createdBrandDto =  _mapper.Map<CreatedBrandDto>(createdBrand);
            return createdBrandDto;
        }
    }

}

