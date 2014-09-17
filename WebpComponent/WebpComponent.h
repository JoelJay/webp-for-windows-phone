#pragma once

#include "ppltasks.h"

using namespace Platform;

namespace WebpComponent
{
	public ref class WebpRuntimeComponent sealed
	{
	public:
		WebpRuntimeComponent();

		Platform::Array<byte>^ DecodeRGBA(const Array<byte> ^data, int data_size, int width, int height);
	};
}