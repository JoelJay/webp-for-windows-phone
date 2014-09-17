// WebpComponent.cpp
#include "pch.h"
#include "WebpComponent.h"
#include "../webp/decode.h"

using namespace WebpComponent;
using namespace Platform;

WebpRuntimeComponent::WebpRuntimeComponent()
{
}

Platform::Array<byte>^ WebpRuntimeComponent::DecodeRGBA(const Array<byte> ^data, int data_size, int width, int height)
{
	byte* result = WebPDecodeRGBA(data->Data, data_size, &width, &height);
	return ref new Array<byte>(result, width*height * 4);
}
